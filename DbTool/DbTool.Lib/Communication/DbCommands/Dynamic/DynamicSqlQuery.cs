using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using DbTool.Lib.Configuration;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Meta;

namespace DbTool.Lib.Communication.DbCommands.Dynamic
{
    public class DynamicSqlQuery
    {
        public ConnectionData ConnectionData { get; set; }
        public DbConnection DbConnection { get; set; }

        public IEnumerable<dynamic> Schema(string collection)
        {
            if (ConnectionData == null)
            {
                return Enumerable.Empty<dynamic>();
            }

			try
			{
				DbConnection.Open();
				return DoGetSchema(DbConnection.GetSchema(collection));
			}
			finally
			{
				DbConnection.Close();
			}
        }

        private static IEnumerable<dynamic> DoGetSchema(DataTable dataTable)
        {
            var columns = GetColumns(dataTable);
            return (from DataRow row in dataTable.Rows select new DynamicDataRow(columns, row));
        }

        private static IEnumerable<string> GetColumns(DataTable dataTable)
        {
            return from DataColumn column in dataTable.Columns select column.ColumnName;
        }
        
        public IEnumerable<dynamic> Query(string sql)
        {
            return DbConnection == null 
                ? Enumerable.Empty<dynamic>()
                : DoQuery(sql);
        }

        private IEnumerable<dynamic> DoQuery(string sql)
        {
            using (var command = DbConnection.CreateCommand())
            {
                command.CommandText = sql.Trim();
                DbConnection.Open();
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var columns = Getcolumns(reader);
                        foreach (IDataRecord row in reader)
                        {
                            yield return new DynamicDataRow(columns, row);
                        }
                    }
                }
                finally
                {
                    DbConnection.Close();
                }
            }
        }

        private static IEnumerable<string> Getcolumns(IDataRecord reader)
        {
            for (var ii=0; ii<reader.FieldCount; ii++)
            {
                yield return reader.GetName(ii);
            }
        }

        public void Insert<T>(T item)
        {
            if (ConnectionData == null)
            {
                return;
            }
            try
            {
                using (var command = DbConnection.CreateCommand())
                {
                    var meta = new TypeMeta(typeof(T));
                    var properties = meta.Properties.Where(p => p.HasGetter).ToList();
                    foreach (var property in properties)
                    {
                        var parameterName = string.Format("@{0}", property.Name);
                        var value = property.GetValue(item) ?? DBNull.Value;
                        command.Parameters.Add(new SqlParameter(parameterName, value));
                    }
                    
                    var insert = new StringBuilder("insert into ").AppendFormat(meta.Type.Name)
                        .AppendFormat(" ({0})", string.Join(", ", properties.Select(p => p.Name)))
                        .Append(" values ")
                        .AppendFormat(" ({0})", string.Join(", ", command.Parameters.Cast<SqlParameter>().Select(p => p.ParameterName)));
                    command.CommandText = insert.ToString();

                    DbConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                DbConnection.Close();
            }
        }
    }
}
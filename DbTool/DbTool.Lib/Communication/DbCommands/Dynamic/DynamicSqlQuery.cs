using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using DbTool.Lib.Configuration;

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
				return DoGetSchema(DbConnection.GetSchema());
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
    }
}
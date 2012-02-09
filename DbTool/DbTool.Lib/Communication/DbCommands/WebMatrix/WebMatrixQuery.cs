using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using WebMatrix.Data;

namespace DbTool.Lib.Communication.DbCommands.WebMatrix
{
    public class WebMatrixQuery
    {
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }

        public WebMatrixQuery()
        {
            ConnectionString = string.Empty;
            ProviderName = string.Empty;
        }

        public IEnumerable<dynamic> Schema(string collection)
        {
            using (var db = Database.OpenConnectionString(ConnectionString, ProviderName))
            {
                try
                {
                    db.Connection.Open();
                    var dataTable = db.Connection.GetSchema(collection);
                    return DoGetSchema(dataTable).ToList().AsReadOnly();    
                }
                finally
                {
                    db.Connection.Close();
                }
                
            }
        }

        private IEnumerable<dynamic> DoGetSchema(DataTable dataTable)
        {
            var columns = GetColumns(dataTable);
            return (from DataRow row in dataTable.Rows select new DynamicDataRow(columns, row));
        }

        private IEnumerable<string> GetColumns(DataTable dataTable)
        {
            return from DataColumn column in dataTable.Columns select column.ColumnName;
        }

        public IEnumerable<dynamic> Query(string sql)
        {
            if (IsSingleWord(sql))
            {
                sql = string.Format("select * from {0}", sql);
            }

            using (var db = Database.OpenConnectionString(ConnectionString, ProviderName))
            {
                var result = db.Query(sql);
                return result;
            }
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            if (IsSingleWord(sql))
            {
                sql = string.Format("select * from {0}", sql);
            }

            using (var db = Database.OpenConnectionString(ConnectionString, ProviderName))
            {
                var result = db.Query(sql).Cast<T>();
                return result;
            }
        }

        private static bool IsSingleWord(string value)
        {
            return Regex.IsMatch(value, @"[^\s]+");
        }
    }
}
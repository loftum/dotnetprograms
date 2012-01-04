using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebMatrix.Data;

namespace DbTool.Lib.CSharp.WebMatrix
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

        public IEnumerable<dynamic> Query(string sql)
        {
            if (IsOneWord(sql))
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
            if (IsOneWord(sql))
            {
                sql = string.Format("select * from {0}", sql);
            }

            using (var db = Database.OpenConnectionString(ConnectionString, ProviderName))
            {
                var result = db.Query(sql).Cast<T>();
                return result;
            }
        }

        private static bool IsOneWord(string value)
        {
            return Regex.IsMatch(value, @"[^\s]+");
        }
    }
}
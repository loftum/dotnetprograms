using System.Collections.Generic;
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
            using (var db = Database.OpenConnectionString(ConnectionString, ProviderName))
            {
                return db.Query(sql);
            }
        }
    }
}
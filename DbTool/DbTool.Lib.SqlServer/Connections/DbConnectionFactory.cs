using System.Data.Common;
using System.Data.SqlClient;
using DbTool.Lib.Configuration;
using DbTool.Lib.Connections;

namespace DbTool.Lib.SqlServer.Connections
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(ConnectionData connectionData)
        {
            return new SqlConnection(connectionData.GetConnectionString());
        }
    }
}
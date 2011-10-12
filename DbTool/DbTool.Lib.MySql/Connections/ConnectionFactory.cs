using System.Data.Common;
using DbTool.Lib.Configuration;
using DbTool.Lib.Connections;
using MySql.Data.MySqlClient;

namespace DbTool.Lib.MySql.Connections
{
    public class ConnectionFactory : IConnectionFactory
    {
        public DbConnection CreateConnection(ConnectionData connectionData)
        {
            return new MySqlConnection(connectionData.GetConnectionString());
        }
    }
}
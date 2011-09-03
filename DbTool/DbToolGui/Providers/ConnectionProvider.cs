using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Configuration;

namespace DbToolGui.Providers
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly IDbToolConfig _config = new DbToolConfig();

        public IEnumerable<string> GetConnectionNames()
        {
            return GetConnections().Select(c => c.Name);
        }

        public string GetDefaultConnectionName()
        {
            return _config.Settings.DefaultConnection.Name;
        }

        public DbConnection GetConnection(string selectedConnection)
        {
            return GetConnections()
                .Where(c => c.Name.Equals(selectedConnection))
                .SingleOrDefault();
        }

        private IEnumerable<DbConnection> GetConnections()
        {
            return _config.Settings.Connections;
        }
    }
}
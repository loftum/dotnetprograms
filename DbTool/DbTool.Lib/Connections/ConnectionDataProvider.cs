using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.Connections
{
    public class ConnectionDataProvider : IConnectionDataProvider
    {
        private readonly IDbToolConfig _config;

        public ConnectionDataProvider(IDbToolConfig config)
        {
            _config = config;
        }

        public IEnumerable<string> GetConnectionNames()
        {
            return GetConnections().Select(c => c.Name);
        }

        public string GetDefaultConnectionName()
        {
            return _config.Settings.DefaultConnection.Name;
        }

        public ConnectionData GetConnection(string selectedConnection)
        {
            return GetConnections()
                .Where(c => c.Name.Equals(selectedConnection))
                .SingleOrDefault();
        }

        private IEnumerable<ConnectionData> GetConnections()
        {
            return _config.Settings.Connections;
        }
    }
}
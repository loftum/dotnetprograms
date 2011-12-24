using System.Collections.Generic;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Configuration
{
    public class DbToolContext
    {
        public string Name { get; set; }
        public string DatabaseType { get; set; }
        public string Host { get; set; }
        public DbToolCredentials Credentials { get; set; }
        private IList<ConnectionData> _connections;
        public IList<ConnectionData> Connections
        {
            get { return _connections; }
            set 
            {
                _connections = value;
                _connections.Each(connection => connection.Parent = this);
            }
        }

        public DbToolContext()
        {
            Connections = new List<ConnectionData>();
        }

        public void AddConnection(ConnectionData connection)
        {
            Connections.Add(connection);
            connection.Parent = this;
        }

        public DbToolContext WithCredentials(DbToolCredentials credentials)
        {
            Credentials = credentials;
            return this;
        }

        public DbToolContext WithConnection(ConnectionData connection)
        {
            AddConnection(connection);
            return this;
        }
    }
}
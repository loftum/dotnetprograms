using Newtonsoft.Json;

namespace DbTool.Lib.Configuration
{
    public class DbToolDatabase
    {
        [JsonIgnore]
        public DbToolContext Parent { get; set; }

        private string _name;
        public string Name
        {
            get { return _name ?? Database; }
            set { _name = value; }
        }

        public string Database { get; set; }
        private string _databaseType;
        public string DatabaseType
        {
            get { return _databaseType ?? Parent.DatabaseType; }
            set { _databaseType = value; }
        }

        private string _host;
        public string Host
        {
            get { return _host ?? Parent.Host; }
            set { _host = value; }
        }

        private DbToolCredentials _credentials;
        public DbToolCredentials Credentials
        {
            get { return _credentials ?? Parent.Credentials; }
            set { _credentials = value; }
        }

        public string MigrationPath { get; set; }

        public ConnectionData GetConnectionData()
        {
            return new ConnectionData
                {
                    Credentials = Credentials,
                    Database = Database,
                    DatabaseType = DatabaseType,
                    Host = Host,
                };
        }
    }
}
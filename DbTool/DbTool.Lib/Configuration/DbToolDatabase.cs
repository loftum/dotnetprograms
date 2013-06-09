using System;
using DotNetPrograms.Common.ExtensionMethods;
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
            get { return _databaseType ?? FromParentOrDefault(p => p.DatabaseType); }
            set { _databaseType = value; }
        }

        private string _host;
        public string Host
        {
            get { return _host ?? FromParentOrDefault(p => p.Host); }
            set { _host = value; }
        }

        private int _port;
        public int Port
        {
            get { return _port > 0 ? _port : FromParentOrDefault(p => p.Port); }
            set { _port = value; }
        }

        private DbToolCredentials _credentials;
        public DbToolCredentials Credentials
        {
            get { return _credentials ?? FromParentOrDefault(p => p.Credentials); }
            set { _credentials = value; }
        }

        public T FromParentOrDefault<T>(Func<DbToolContext,T> propertyFunc, T defaultValue = default(T))
        {
            return Parent == null
                ? default(T)
                : propertyFunc(Parent);
        }

        public string MigrationType { get; set; }
        public string MigrationPath { get; set; }

        [JsonIgnore]
        public bool CanMigrate
        {
            get { return !(MigrationPath.IsNullOrEmpty() || MigrationType.IsNullOrEmpty()); }
        }

        public ConnectionData GetConnectionData()
        {
            return new ConnectionData
                {
                    Name = Name,
                    Credentials = Credentials,
                    Database = Database,
                    DatabaseType = DatabaseType,
                    Host = Host,
                };
        }
    }
}
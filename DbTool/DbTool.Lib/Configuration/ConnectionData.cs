using System.Collections.Generic;
using DbTool.Lib.ExtensionMethods;
using Newtonsoft.Json;

namespace DbTool.Lib.Configuration
{
    public class ConnectionData
    {
        [JsonIgnore]
        public DbToolContext Parent { get; set; }

        [JsonIgnore]
        public bool HasConnectionString
        {
            get { return GetConnectionString().IsNotNullOrEmpty(); }
        }

        private string _name;
        public string Name
        {
            get { return _name ?? Database; }
            set { _name = value; }
        }
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

        public string Database { get; set; }

        private DbToolCredentials _credentials;
        public DbToolCredentials Credentials
        {
            get
            {
                if (_credentials != null)
                {
                    return _credentials;
                }
                return Parent == null ? null : Parent.Credentials;
            }
            set { _credentials = value; }
        }

        public bool Default { get; set; }
        public string MigrationPath { get; set; }

        public string GetConnectionString(bool includeDatabase = true)
        {
            switch(DatabaseType)
            {
                case "mysql":
                    return GetConnectionStringForMySql(includeDatabase);
                default:
                    return GetDefaultConnectionString(includeDatabase);
            }
        }

        private string GetDefaultConnectionString(bool includeDatabase)
        {
            var elements = new List<string>();
            elements.Add(string.Format("Data Source={0}", Host));
            if (includeDatabase && Database.IsNotNullOrEmpty())
            {
                elements.Add(string.Format("Initial Catalog={0}", Database));
            }
            if (Credentials.IntegratedSecurity)
            {
                elements.Add(string.Format("Integrated Security={0}", Credentials.IntegratedSecurity));
            }
            else
            {
                elements.Add(string.Format("User Id={0}", Credentials.User));
                elements.Add(string.Format("Password={0}", Credentials.Password));
            }
            return string.Join(";", elements);
        }

        private string GetConnectionStringForMySql(bool includeDatabase)
        {
            var elements = new List<string>();
            elements.Add(string.Format("Server={0}", Host));
            if (includeDatabase && Database.IsNotNullOrEmpty())
            {
                elements.Add(string.Format("Database={0}", Database));
            }
            elements.Add(string.Format("Uid={0}", Credentials.User));
            elements.Add(string.Format("Pwd={0}", Credentials.Password));
            return string.Join(";", elements);
        }
    }
}
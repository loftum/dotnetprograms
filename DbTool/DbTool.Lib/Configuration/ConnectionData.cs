using System.Collections.Generic;
using DbTool.Lib.ExtensionMethods;
using Newtonsoft.Json;

namespace DbTool.Lib.Configuration
{
    public class ConnectionData
    {
        [JsonIgnore]
        public bool HasConnectionString
        {
            get { return GetConnectionString().IsNotNullOrEmpty(); }
        }

        public string Name { get; set; }
        public string Database { get; set; }
        public string DatabaseType { get; set; }
        public string Host { get; set; }
        public DbToolCredentials Credentials { get; set; }

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
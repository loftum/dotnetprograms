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
        public string DatabaseType { get; set; }
        public string Host { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool IntegratedSecurity { get; set; }
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
            if (IntegratedSecurity)
            {
                elements.Add(string.Format("Integrated Security={0}", IntegratedSecurity));
            }
            else
            {
                elements.Add(string.Format("User Id={0}", User));
                elements.Add(string.Format("Password={0}", Password));
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
            elements.Add(string.Format("Uid={0}", User));
            elements.Add(string.Format("Pwd={0}", Password));
            return string.Join(";", elements);
        }
    }
}
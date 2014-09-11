using System.Collections.Generic;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Lib.Configuration
{
    public class ConnectionData
    {
        public bool HasConnectionString
        {
            get { return GetConnectionString().IsNotNullOrEmpty(); }
        }

        public string Name { get; set; }
        public string Database { get; set; }
        public string DatabaseType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public DbToolCredentials Credentials { get; set; }

        public string GetConnectionString()
        {
            var elements = new List<string>();
            elements.Add(string.Format("Data Source={0}", Host));
            if (Database.IsNotNullOrEmpty())
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
    }
}
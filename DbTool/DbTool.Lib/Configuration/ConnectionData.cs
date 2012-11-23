using System.Collections.Generic;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Lib.Configuration
{
    public class ConnectionData
    {
        public string ProviderName
        {
            get
            {
                switch (DatabaseType)
                {
                    case DbType.SqlServer:
                    case DbType.SqlServer2005:
                    case DbType.SqlServer2008:
                        return "System.Data.SqlClient";
                    case DbType.MySql:
                        return "MySql.Data.MySqlClient";
                    default:
                        return "System.Data.SqlClient";
                }
            }
        }

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
            switch(DatabaseType)
            {
                case DbType.RavenDB:
                    return GetRavenConnectionString();
                default:
                    return GetDefaultConnectionString();
            }
        }

        private string GetRavenConnectionString()
        {
            var elements = new List<string>();
            elements.Add(string.Format("Url={0}", GetHttpHostAndPort()));
            if (Database.IsNotNullOrEmpty())
            {
                elements.Add(string.Format("Database={0}", Database));
            }
            if (Credentials.User.IsNotNullOrEmpty())
            {
                elements.Add(string.Format("User={0}", Credentials.User));
                elements.Add(string.Format("Password={0}", Credentials.Password));
            }
            return string.Join(";", elements);
        }

        private string GetHttpHostAndPort()
        {
            var host = Host.StartsWith("http://") ? Host : string.Format("http://{0}", Host);
            return Port > 0 ? string.Format("{0}:{1}", host, Port) : host;
        }

        private string GetDefaultConnectionString()
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
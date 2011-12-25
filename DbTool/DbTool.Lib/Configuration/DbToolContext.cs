using System.Collections.Generic;

namespace DbTool.Lib.Configuration
{
    public class DbToolContext
    {
        public string Name { get; set; }
        public string DatabaseType { get; set; }
        public string Host { get; set; }
        public DbToolCredentials Credentials { get; set; }
        public IList<DbToolDatabase> Databases { get; set; }

        public DbToolContext(string name) : this()
        {
            Name = name;
        }

        public DbToolContext()
        {
            Databases = new List<DbToolDatabase>();
        }

        public DbToolContext WithCredentials(DbToolCredentials credentials)
        {
            Credentials = credentials;
            return this;
        }

        public DbToolContext WithDatabase(DbToolDatabase connection)
        {
            Databases.Add(connection);
            connection.Parent = this;
            return this;
        }

        public ConnectionData GetDefaultConnection()
        {
            return new ConnectionData
                {
                    DatabaseType = DatabaseType,
                    Host = Host,
                    Credentials = Credentials
                };
        }

        public string ConnectionString
        {
            get { return GenerateConnectionString(); }
        }

        private string GenerateConnectionString()
        {
            var elements = new List<string> {string.Format("Data Source={0}", Host)};

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
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

        public DbToolContext WithDatabase(DbToolDatabase database)
        {
            Databases.Add(database);
            database.Parent = this;
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
    }
}
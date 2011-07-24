using System;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace StuffLibrary.Migrations
{
    public class Migrator
    {
        private readonly string _databaseProvider;
        private readonly string _connectionString;

        public Migrator(string databaseProvider, string connectionString)
        {
            _databaseProvider = databaseProvider;
            _connectionString = connectionString;
        }

        public void MigrateTo(long version, bool showSql = false)
        {
            using (var announcer = new TextWriterAnnouncer(Console.Out) { ShowSql = showSql })
            {
                var context = new RunnerContext(announcer)
                {
                    Database = _databaseProvider,
                    Connection = _connectionString,
                    Target = typeof(M001InitialVersion).Assembly.ToString(),
                    Version = version
                };
                var executor = new TaskExecutor(context);
                executor.Execute();
            }
        }

        public void MigrateToLatest(bool showSql = false)
        {
            MigrateTo(0);
        }
    }
}
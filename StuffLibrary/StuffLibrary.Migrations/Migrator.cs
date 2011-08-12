using System;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Processors;

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
                var runner = CreateRunner(announcer);
                var latest = runner.VersionInfo.Latest();
                if (latest < version)
                {
                    runner.MigrateUp(version);
                }
                else if (latest > version)
                {
                    runner.RollbackToVersion(version);
                }
                else
                {
                    announcer.Say(string.Format("Already at version {0}", latest));
                }
            }
        }

        public void MigrateToLatest(bool showSql = false)
        {
            using (var announcer = new TextWriterAnnouncer(Console.Out) { ShowSql = showSql })
            {
                var runner = CreateRunner(announcer);
                runner.MigrateUp();
            }
        }

        public void Rollback(bool showSql = false)
        {
            using (var announcer = new TextWriterAnnouncer(Console.Out) { ShowSql = showSql })
            {
                var runner = CreateRunner(announcer);
                runner.RollbackToVersion(0);
            }
        }

        private IMigrationVersionRunner CreateRunner(IAnnouncer announcer)
        {
            var migrationConventions = new MigrationConventions();
            var processor = ProcessorFactory.GetFactory(_databaseProvider)
                .Create(_connectionString, announcer, new ProcessorOptions { PreviewOnly = false });

            return new MigrationVersionRunner(new MigrationConventions(),
                                                    processor,
                                                    new MigrationLoader(migrationConventions),
                                                    typeof(M001InitialVersion).Assembly,
                                                    null,
                                                    announcer,
                                                    null);
        }
    }
}
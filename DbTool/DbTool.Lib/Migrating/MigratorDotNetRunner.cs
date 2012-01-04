using System.Reflection;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Migrator.Framework.Loggers;

namespace DbTool.Lib.Migrating
{
    public class MigratorDotNetRunner : IMigrateDbTask
    {
        private readonly DbToolDatabase _database;
        private readonly IDbToolLogger _logger;

        public MigratorDotNetRunner(DbToolDatabase database, IDbToolLogger logger)
        {
            _database = database;
            _logger = logger;
        }

        public void MigrateTo(long version)
        {
            CreateMigrator().MigrateTo(version);
        }

        public void MigrateToLatest()
        {
            CreateMigrator().MigrateToLastVersion();
        }

        private global::Migrator.Migrator CreateMigrator()
        {
            var connectionString = _database.GetConnectionData().GetConnectionString();
            var assembly = Assembly.LoadFrom(_database.MigrationPath);
            return new global::Migrator.Migrator("sqlserver", connectionString, assembly, false, new Logger(false, _logger));
        }
    }
}
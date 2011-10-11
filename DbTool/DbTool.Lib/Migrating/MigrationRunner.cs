using System.Reflection;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Migrator.Framework.Loggers;

namespace DbTool.Lib.Migrating
{
    public class MigrationRunner : IMigrateDbTask
    {
        private readonly ConnectionData _connectionData;
        private readonly IDbToolLogger _logger;

        public MigrationRunner(ConnectionData connectionData, IDbToolLogger logger)
        {
            _connectionData = connectionData;
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

        private Migrator.Migrator CreateMigrator()
        {
            var connectionString = _connectionData.GetConnectionString();
            var assembly = Assembly.LoadFrom(_connectionData.MigrationPath);
            return new Migrator.Migrator("sqlserver", connectionString, assembly, false, new Logger(false, _logger));
        }
    }
}
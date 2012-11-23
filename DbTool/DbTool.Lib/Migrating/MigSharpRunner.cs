using System.Reflection;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Tasks;
using DotNetPrograms.Common.ExtensionMethods;
using MigSharp;

namespace DbTool.Lib.Migrating
{
    public class MigSharpRunner : IMigrateDbTask
    {
        private readonly DbToolDatabase _database;

        public MigSharpRunner(DbToolDatabase database)
        {
            _database = database;
        }

        public void MigrateTo(long version)
        {
            CreateMigrator().MigrateTo(Assembly.LoadFile(_database.MigrationPath), version);
        }

        public void MigrateToLatest()
        {
            CreateMigrator().MigrateAll(Assembly.LoadFile(_database.MigrationPath));
        }

        private MigSharp.Migrator CreateMigrator()
        {
            var providerName = DbType.ToMigSharpProvider(_database.DatabaseType);
            var connection = _database.GetConnectionData();
            var options = new MigrationOptions();
            options.SupportedProviders.Set(providerName.AsArray());
            var migrator = new MigSharp.Migrator(connection.GetConnectionString(), providerName, options);
            return migrator;
        }
    }
}
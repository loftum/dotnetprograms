using System.Reflection;
using DbTool.Lib.Configuration;
using DbTool.Lib.Tasks;

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
            var providerName = DatabaseType.ToMigSharpProvider(_database.DatabaseType);
            var connection = _database.GetConnectionData();
            var migrator = new MigSharp.Migrator(connection.GetConnectionString(), providerName);
            return migrator;
        }
    }
}
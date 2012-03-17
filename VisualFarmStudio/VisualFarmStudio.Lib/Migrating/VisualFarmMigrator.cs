using System.Data.SqlClient;
using MigSharp;
using VisualFarmStudio.Common.Configuration;
using VisualFarmStudio.Common.ExtensionMethods;
using VisualFarmStudio.Migrations.Steps;

namespace VisualFarmStudio.Lib.Migrating
{
    public class VisualFarmMigrator : IVisualFarmMigrator
    {
        private readonly IVFSConfig _config;
        private readonly MigrationOptions _options;
        public VisualFarmMigrator()
        {
            _config = new VFSConfig();
            _options = new MigrationOptions();
            _options.SupportedProviders.Set(ProviderNames.SqlServer2008.AsArray());
        }

        public void MigrateUp()
        {
            var migrator = new Migrator(_config.ConnectionString, ProviderNames.SqlServer2008, _options);
            migrator.MigrateAll(typeof(V001_Initial_1).Assembly);
        }

        public void MigrateDown()
        {
            var migrator = new Migrator(_config.ConnectionString, ProviderNames.SqlServer2008, _options);
            migrator.MigrateTo(typeof(V001_Initial_1).Assembly, 0);
        }

        public string GetVersion()
        {
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select max(timestamp) from MigSharp";
                    return command.ExecuteScalar().ToString();
                }
            }
        }
    }
}
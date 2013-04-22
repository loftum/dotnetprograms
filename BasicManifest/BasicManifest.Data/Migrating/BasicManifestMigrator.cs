using System.Data.SqlClient;
using System.Reflection;
using BasicManifest.Common.Configuration;
using BasicManifest.Migrations.Steps;
using DotNetPrograms.Common.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Data.Migrating
{
    public class BasicManifestMigrator : IBasicManifestMigrator
    {
        private readonly IBMConfig _config;
        private readonly MigrationOptions _options;
        private readonly Assembly _migrationAssembly = typeof (M_000_Initial0).Assembly;

        public BasicManifestMigrator(IBMConfig config)
        {
            _config = config;
            _options = new MigrationOptions();
            _options.SupportedProviders.Set(ProviderNames.SqlServer2008.AsArray());
        }

        public long MigrateUp()
        {
            var migrator = GetMigrator();
            migrator.MigrateAll(_migrationAssembly);
            return GetVersion();
        }

        private Migrator GetMigrator()
        {
            return new Migrator(_config.BasicManifestConnectionString, ProviderNames.SqlServer2008, _options);
        }

        public long GetVersion()
        {
            using (var connection = new SqlConnection(_config.BasicManifestConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select max(timestamp) from MigSharp";
                    return command.ExecuteScalar().ConvertTo<long>();
                }
            }
        }

        public long MigrateTo(long version)
        {
            var migrator = GetMigrator();
            migrator.MigrateTo(_migrationAssembly, version);
            return GetVersion();
        }
    }
}
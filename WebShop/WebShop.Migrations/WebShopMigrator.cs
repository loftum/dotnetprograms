using System.Data.SqlClient;
using MigSharp;
using WebShop.Migrations.Steps;

namespace WebShop.Migrations
{
    public class WebShopMigrator : IWebShopMigrator
    {
        private readonly string _connectionString;

        public WebShopMigrator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public long MigrateUp()
        {
            var migrator = GetMigrator();
            migrator.MigrateAll(typeof(M_000_Initial0).Assembly);
            return GetCurrentVersion();
        }

        public long MigrateDown()
        {
            return MigrateTo(0);
        }

        public long MigrateTo(long version)
        {
            var migrator = GetMigrator();
            migrator.MigrateTo(typeof(M_000_Initial0).Assembly, version);
            return GetCurrentVersion();
        }

        private long GetCurrentVersion()
        {
            object value;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select max(Timestamp) from MigSharp";
                    connection.Open();
                    value = command.ExecuteScalar();
                    connection.Close();
                }
            }
            return value == null ? 0 : (long) value;
        }

        private Migrator GetMigrator()
        {
            var options = new MigrationOptions();
            options.SupportedProviders.Set(new []{ProviderNames.SqlServer2008});
            return new Migrator(_connectionString, ProviderNames.SqlServer2008, options);
        }
    }
}
using NUnit.Framework;
using WebShop.Common.Configuration;
using WebShop.Migrations;

namespace WebShop.IntegrationTesting.Core.Data
{
    [TestFixture]
    public class WebShopMigrationTest
    {
        [Test]
        public void MigrateUp()
        {
            GetRunner().MigrateUp();
        }

        [Test]
        public void MigrateDown()
        {
            GetRunner().MigrateDown();
        }

        [Test]
        public void MigrateToVersion()
        {
            GetRunner().MigrateTo(1);
        }

        private static WebShopMigrator GetRunner()
        {
            return new WebShopMigrator(new ConfigSettings().TransactionsConnectionString);
        }
    }
}
using NUnit.Framework;
using WebShop.Common.Configuration;
using WebShop.Migrations;

namespace WebShop.IntegrationTesting.Core.Data
{
    [TestFixture]
    public class MigrationTest
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

        private static MigrationRunner GetRunner()
        {
            return new MigrationRunner(new ConfigSettings().MasterDataConnectionString);
        }
    }
}
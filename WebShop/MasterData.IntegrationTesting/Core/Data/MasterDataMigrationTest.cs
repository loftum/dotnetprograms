using MasterData.Migrations;
using NUnit.Framework;
using WebShop.Common.Configuration;

namespace MasterData.IntegrationTesting.Core.Data
{
    [TestFixture]
    public class MasterDataMigrationTest
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
            GetRunner().MigrateTo(11);
        }

        private static MasterDataMigrator GetRunner()
        {
            return new MasterDataMigrator(new ConfigSettings().MasterDataConnectionString);
        } 
    }
}
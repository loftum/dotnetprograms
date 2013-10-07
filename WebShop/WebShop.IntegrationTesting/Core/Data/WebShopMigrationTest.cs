using System;
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
            var version = GetRunner().MigrateUp();
            Console.WriteLine("Migrated to {0}", version);
        }

        [Test]
        public void MigrateDown()
        {
            var version = GetRunner().MigrateDown();
            Console.WriteLine("Migrated to {0}", version);
        }

        [Test]
        public void MigrateToVersion()
        {
            var version = GetRunner().MigrateTo(1);
            Console.WriteLine("Migrated to {0}", version);
        }

        private static WebShopMigrator GetRunner()
        {
            return new WebShopMigrator(new ConfigSettings().OrderDbConnectionString);
        }
    }
}
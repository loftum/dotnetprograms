using System;
using NUnit.Framework;
using StuffLibrary.Common.Configuration;
using StuffLibrary.Migrations;

namespace StuffLibrary.IntegrationTesting
{
    [TestFixture]
    public class MigrationTest
    {
        [Test]
        public void ShouldMigrateUp()
        {
            var config = StuffLibraryConfig.Instance;
            Console.WriteLine(config.Databaseprovider);
            Console.WriteLine(config.ConnectionString);
            var migrationRunner = new Migrator(config.Databaseprovider, config.ConnectionString);
            migrationRunner.MigrateToLatest();
        }
    }
}
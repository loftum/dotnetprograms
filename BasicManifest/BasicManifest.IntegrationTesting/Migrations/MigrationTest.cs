using System;
using BasicManifest.Data.Migrating;
using BasicManifest.IntegrationTesting.Ioc;
using BasicManifest.Web.Ioc;
using NUnit.Framework;

namespace BasicManifest.IntegrationTesting.Migrations
{
    [TestFixture]
    public class MigrationTest
    {
        [SetUp]
        public void Setup()
        {
            Lifecycle.Current = new IntegrationTestLifecycle();
            ObjectContainer.Init(new BMRegistry());
        }

        [Test]
        public void MigrateUp()
        {
            var migrator = ObjectContainer.Get<BasicManifestMigrator>();
            var version = migrator.MigrateUp();
            Console.WriteLine("Migrated to {0}", version);
        }

        [Test]
        public void MigrateDown()
        {
            var migrator = ObjectContainer.Get<BasicManifestMigrator>();
            var version = migrator.MigrateTo(0);
            Console.WriteLine("Migrated to {0}", version);
        }

        [Test]
        public void MigrateTo()
        {
            const long to = 42;
            var migrator = ObjectContainer.Get<BasicManifestMigrator>();
            var version = migrator.MigrateTo(to);
            Console.WriteLine("Migrated to {0}", version);
        }
    }
}
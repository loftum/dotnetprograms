using NUnit.Framework;
using StructureMap;
using WebShop.Common.Configuration;
using WebShop.IntegrationTesting.IoC;
using WebShop.Migrations;
using WebShop.Web.IoC;

namespace WebShop.IntegrationTesting
{
    [TestFixture]
    public abstract class DbTestBase
    {
        [TestFixtureSetUp]
        public void SetUpTestFixture()
        {
            new WebShopMigrator(new ConfigSettings().MasterDataConnectionString).MigrateUp();
            Lifecycle.Current = new IntegrationTestLifecycle();
            ObjectFactory.Initialize(a => a.AddRegistry(new WebShopRegistry()));
        }
    }
}
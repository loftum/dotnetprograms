using MasterData.IntegrationTesting.IoC;
using MasterData.Migrations;
using MasterData.Web.IoC;
using NUnit.Framework;
using StructureMap;
using WebShop.Common.Configuration;

namespace MasterData.IntegrationTesting
{
    public class DbTestBase
    {
        [TestFixtureSetUp]
        public void SetUpTestFixture()
        {
            new MasterDataMigrator(new ConfigSettings().MasterDataConnectionString).MigrateUp();
            Lifecycle.Current = new IntegrationTestLifecycle();
            ObjectFactory.Initialize(a => a.AddRegistry(new MasterDataRegistry()));
        } 
    }
}
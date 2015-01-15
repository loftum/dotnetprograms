using NUnit.Framework;
using StructureMap;
using WebShop.Common.Configuration;
using WebShop.Core.Data;
using WebShop.Core.Domain.OrderDb;
using WebShop.IntegrationTesting.IoC;
using WebShop.Migrations;
using WebShop.UnitTesting.TestData;
using WebShop.Web.IoC;

namespace WebShop.IntegrationTesting.Core.Data
{
    [TestFixture]
    public class OrderDbRepositoryTest
    {
        private OrderRepository _repo;

        [TestFixtureSetUp]
        public void SetUpTestFixture()
        {
            new WebShopMigrator(new ConfigSettings().OrderDbConnectionString).MigrateUp();
            Lifecycle.Current = new IntegrationTestLifecycle();
            ObjectFactory.Initialize(a =>
                {
                    a.AddRegistry(new WebShopRegistry());
                    a.AddRegistry(new IntegrationTestRegistry());
                });
        }

        [SetUp]
        public void Setup()
        {
            _repo = ObjectFactory.GetInstance<OrderRepository>();
        }

        [Test]
        public void SaveOrder()
        {
            var order = new OrderHead();
            order.Buyer.FirstName = Some.FirstName;
            order.Buyer.LastName = Some.LastName;
            order.Buyer.Email = Some.Email;
            order.Buyer.PhoneNumber = Some.PhoneNumber;
                

            _repo.Save(order);
            _repo.Commit();
        }
    }
}
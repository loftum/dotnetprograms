using System.Linq;
using MissingLinq.Sql.ExtensionMethods;
using NUnit.Framework;
using StructureMap;
using WebShop.Core.Data;
using WebShop.Core.Domain.MasterData;

namespace WebShop.IntegrationTesting.Core.Data
{
    [TestFixture]
    public class MasterDataRepositoryTest : DbTestBase
    {
        private MasterDataRepository _repo;
        private SqlDatabase _db;

        [SetUp]
        public void Setup()
        {
            _repo = ObjectFactory.GetInstance<MasterDataRepository>();
            _db = ObjectFactory.GetInstance<SqlDatabase>();
        }

        [Test]
        public void SaveProduct()
        {
            var parent = new Product
                {
                    Name = "Produkt",
                    ProductNumber = "1234"
                };
            parent.Description.Value = "Description";

            _repo.Save(parent);
            var child = new Product
            {
                Name = "Produkt",
                ProductNumber = "1234"
            };
            child.Description.Reset();

            parent.Add(child);

            _repo.Commit();
        }

        [Test]
        public void VerifyDescription()
        {
            var child = _repo.GetAll<Product>().First(p => p.ProductNumber == "1234" && p.Parent != null);
            Assert.That(child.Description.Calculated, Is.EqualTo("Parent"));
        }

        [Test]
        public void SetDescription()
        {
            var child = _repo.GetAll<Product>().First(p => p.ProductNumber == "1234" && p.Parent != null);
            child.Description.Value = null;
            _repo.Commit();
        }

        [Test]
        public void SetDescriptionOnParent()
        {
            var child = _repo.GetAll<Product>().First(p => p.ProductNumber == "1234" && p.Parent == null);
            child.Description.Value = "Parent";
            _repo.Commit();
        }

        [Test]
        public void DeleteProducts()
        {
            _db.GetAll<Product>().Delete();
        }
    }
}
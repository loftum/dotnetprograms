using System.Linq;
using DotNetPrograms.Common.Meta;
using MasterData.Core.Data;
using MasterData.Core.Domain.MasterData;
using MasterData.Core.Domain.Pricing;
using MasterData.UnitTesting.TestData;
using MissingLinq.Sql.Data;
using MissingLinq.Sql.ExtensionMethods;
using NUnit.Framework;
using StructureMap;

namespace MasterData.IntegrationTesting.Core.Domain
{
    [TestFixture]
    public class MasterDataRepositoryTest : DbTestBase
    {
        private MasterDataRepository _repo;
        private MissingLinqDatabase _db;

        [SetUp]
        public void Setup()
        {
            _repo = ObjectFactory.GetInstance<MasterDataRepository>();
            _db = ObjectFactory.GetInstance<MissingLinqDatabase>();
        }

        [Test]
        public void SaveProduct()
        {
            var parent = new ProductMaster
                {
                    ProductNumber = "1234",
                    Name = "Produkt",
                    Description = "Description"
                };
            

            _repo.Save(parent);
            var variant = new ProductVariant
                {
                    Name = "Variant"
                };

            parent.Add(variant);

            var saleProduct = new SaleProduct
                {
                };

            variant.Add(saleProduct);

            _repo.Commit();
        }

        [Test]
        public void GenerateProducts()
        {
            for (var ii = 1; ii < 30; ii++)
            {
                Create(ii);
            }
        }

        private void Create(int ii)
        {
            var parent = new ProductMaster
            {
                ProductNumber = "" + ii,
                Name = "Produkt " + ii,
                Description = LoremIpsum.Text
            };

            _repo.Save(parent);
            foreach (var color in new EnumMeta<Color>().GetValues())
            {
                var variant = new ProductVariant
                {
                    Color = color
                };
                parent.Add(variant);

                var saleProduct = new SaleProduct
                {
                    BasePrice = Price.FromExVat(ii + 100, 1.25m)
                };
                variant.Add(saleProduct);
            }
            _repo.Commit();
        }

        [Test]
        public void AlterProduct()
        {
            var variant = _repo.GetAll<ProductMaster>().First(p => p.ProductNumber == "1234").Variants.First();
            variant.Name = "Blah";
            _repo.Commit();
        }

        [Test]
        public void VerifyDescription()
        {
            var child = _repo.GetAll<ProductMaster>().First(p => p.ProductNumber == "1234").Variants.First();
            Assert.That(child.GetDescription().Value, Is.EqualTo("Parent"));
        }

        [Test]
        public void SetDescription()
        {
            var child = _repo.GetAll<ProductMaster>().First(p => p.ProductNumber == "1234").Variants.First();
            child.Description = null;
            _repo.Commit();
        }

        [Test]
        public void SetDescriptionOnParent()
        {
            var child = _repo.GetAll<ProductMaster>().First(p => p.ProductNumber == "1234").Variants.First();
            child.Description = "Parent";
            _repo.Commit();
        }

        [Test]
        public void DeleteProducts()
        {
            _db.GetAll<SaleProduct>().Delete();
            _db.GetAll<ProductVariant>().Delete();
            _db.GetAll<ProductMaster>().Delete();
        }
    }
}
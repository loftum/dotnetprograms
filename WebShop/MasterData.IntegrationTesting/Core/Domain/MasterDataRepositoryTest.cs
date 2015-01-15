using System.Linq;
using DotNetPrograms.Common.Meta;
using MasterData.Core.Data;
using MasterData.Core.Domain.Pricing;
using MasterData.Core.Domain.Products;
using MasterData.Core.Domain.Stores;
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
                    Name = "Produkt",
                    Description = "Description"
                };
            

            _repo.Save(parent);
            var variant = new ProductVariant(parent)
                {
                };

            parent.Add(variant);

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
            var producer = new Producer{ Name = "Producer" };
            _repo.Save(producer);
            var parent = new ProductMaster
            {
                Name = "Produkt " + ii,
                Description = LoremIpsum.Text
            };

            _repo.Save(parent);
            foreach (var color in new EnumMeta<Color>().GetValues())
            {
                var variant = new ProductVariant(parent)
                {
                    Color = color
                };
                parent.Add(variant);
            }
            _repo.Commit();
        }

        [Test]
        public void DeleteProducts()
        {
            _db.GetAll<StoreProduct>().Delete();
            _db.GetAll<ProductVariant>().Delete();
            _db.GetAll<ProductMaster>().Delete();
        }
    }
}
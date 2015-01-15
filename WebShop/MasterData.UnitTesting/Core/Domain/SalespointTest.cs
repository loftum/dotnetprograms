using MasterData.Core.Domain.Stores;
using MasterData.UnitTesting.TestData.Builders;
using NUnit.Framework;

namespace MasterData.UnitTesting.Core.Domain
{
    [TestFixture]
    public class SalespointTest
    {
        private Producer _producer;
        private Supplier _supplier;
        private Reseller _reseller;
        private Salespoint _salespoint;

        [SetUp]
        public void Setup()
        {
            _producer = Build.A<Producer>();
            _supplier = Build.A<Supplier>();
            _reseller = Build.Reseller()
                .WithSupplier(_supplier);
            _salespoint = Build.Salespoint()
                .ForReseller(_reseller);
        }

        [Test]
        public void Assortment_IsEmpty_WhenThereAreNoProducts()
        {
            var assortment = _salespoint.GetAssortment();
            Assert.That(assortment.StoreProducts, Is.Empty);
        }

        [Test]
        public void Assortment_IsEmpty_WhenResellerBlocksProduct()
        {
            _supplier.Add(BuildSupplierProduct());

            _reseller.SaleProductFilter.DefaultInclude = false;
            _salespoint.SaleProductFilter.DefaultInclude = true;
            var assortment = _salespoint.GetAssortment();
            Assert.That(assortment.StoreProducts, Is.Empty);
        }

        [Test]
        public void Assortment_IsEmpty_WhenSalespointBlocksProduct()
        {
            _supplier.Add(BuildSupplierProduct());

            _reseller.SaleProductFilter.DefaultInclude = true;
            _salespoint.SaleProductFilter.DefaultInclude = false;
            var assortment = _salespoint.GetAssortment();
            Assert.That(assortment.StoreProducts, Is.Empty);
        }

        [Test]
        public void Assortment_ContainsProduct_WhenFilterAllowsIt()
        {
            _supplier.Add(BuildSupplierProduct());

            _reseller.SaleProductFilter.DefaultInclude = true;
            _salespoint.SaleProductFilter.DefaultInclude = true;
            var assortment = _salespoint.GetAssortment();
            Assert.That(assortment.StoreProducts, Has.Count.EqualTo(1));
        }

        private SupplierProductBuilder BuildSupplierProduct()
        {
            var master = Build.ProductMaster()
                .WithProducer(_producer);
            var variant = Build.ProductVariant()
                .ForMaster(master);
            return Build.SupplierProduct().ForVariant(variant);
        }
    }
}
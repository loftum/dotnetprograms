using MasterData.Core.Domain.MasterData;
using MasterData.Core.Domain.Pricing;
using MasterData.UnitTesting.TestData.Builders;
using NUnit.Framework;

namespace MasterData.UnitTesting.Core.Domain
{
    [TestFixture]
    public class SaleProductTest
    {
        private SaleProduct _saleProduct;

        [SetUp]
        public void Setup()
        {
            _saleProduct = Build.SaleProduct();
        }

        [Test]
        public void CalculatedPrice_IsMultiplied()
        {
            _saleProduct.BasePrice = Price.FromExVat(100, 1.25m);
            _saleProduct.Calculator.Add(new MultiplyCalculator(1.30m));
            _saleProduct.Calculator.Add(new MultiplyCalculator(2m));

            var price = _saleProduct.CalculatedPrice;
            Assert.That(price.ExVat, Is.EqualTo(260m));
        }

        [Test]
        public void CalculatedPRice_EqualsBasePrice_WhenThereAreNoCalculators()
        {
            _saleProduct.BasePrice = Price.FromExVat(100, 1.25m);

            var price = _saleProduct.CalculatedPrice;
            Assert.That(price.ExVat, Is.EqualTo(100m));
        }
    }
}
using NUnit.Framework;
using WebShop.Core.Domain.MasterData;

namespace WebShop.UnitTesting.Core.Domain
{
    [TestFixture]
    public class PriceTest
    {
        [Test]
        public void Sum_SumsIncVatAndExVat()
        {
            var price = new Price(10m, 8m);
            var price2 = new Price(10m, 8m);
            var sum = price + price2;
            
            Assert.That(sum.IncVat, Is.EqualTo(20m));
            Assert.That(sum.ExVat, Is.EqualTo(16m));
        }
    }
}
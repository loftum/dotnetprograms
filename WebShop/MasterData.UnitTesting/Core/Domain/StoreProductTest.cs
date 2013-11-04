using MasterData.Core.Domain.Products;
using MasterData.UnitTesting.TestData.Builders;
using NUnit.Framework;

namespace MasterData.UnitTesting.Core.Domain
{
    [TestFixture]
    public class StoreProductTest
    {
        private StoreProduct _storeProduct;

        [SetUp]
        public void Setup()
        {
            _storeProduct = Build.StoreProduct();
        }

    }
}
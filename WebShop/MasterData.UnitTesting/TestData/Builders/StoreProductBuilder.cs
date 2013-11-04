using MasterData.Core.Domain.Products;

namespace MasterData.UnitTesting.TestData.Builders
{
    public class StoreProductBuilder : ProductBuilder<StoreProduct, StoreProductBuilder>
    {
        public StoreProductBuilder(StoreProduct item, bool generateId = true) : base(item, generateId)
        {
        }
    }
}
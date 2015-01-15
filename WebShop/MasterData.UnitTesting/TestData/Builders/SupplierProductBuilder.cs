using MasterData.Core.Domain.Products;

namespace MasterData.UnitTesting.TestData.Builders
{
    public class SupplierProductBuilder : MasterDataObjectBuilderBase<SupplierProduct, SupplierProductBuilder>
    {
        public SupplierProductBuilder(SupplierProduct item, bool generateId = true) : base(item, generateId)
        {
        }

        public SupplierProductBuilder ForVariant(ProductVariant variant)
        {
            variant.Add(Item);
            return MySelf;
        }
    }
}
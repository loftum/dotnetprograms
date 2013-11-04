using MasterData.Core.Domain.Products;

namespace MasterData.UnitTesting.TestData.Builders
{
    public class ProductVariantBuilder : MasterDataObjectBuilderBase<ProductVariant, ProductVariantBuilder>
    {
        public ProductVariantBuilder(ProductVariant item, bool generateId = true) : base(item, generateId)
        {
        }

        public ProductVariantBuilder ForMaster(ProductMaster master)
        {
            master.Add(Item);
            return MySelf;
        }
    }
}
using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Mappings
{
    public class ProductMasterMap : ProductMapBase<ProductMaster>
    {
        public ProductMasterMap()
        {
            References(p => p.Producer).Not.Nullable();
            References(p => p.ProductType).Not.Nullable();
            HasMany(p => p.Variants).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
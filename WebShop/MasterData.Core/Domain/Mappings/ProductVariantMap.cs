using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Mappings
{
    public class ProductVariantMap : ProductMapBase<ProductVariant>
    {
        public ProductVariantMap()
        {
            References(p => p.Master, "ProductMaster_Id").Not.Nullable();
            References(p => p.Color).Not.Nullable();
            Map(p => p.ProductNumber).Unique();
            HasMany(p => p.SupplierProducts).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
using MasterData.Core.Domain.MasterData;

namespace MasterData.Core.Domain.Mappings
{
    public class ProductVariantMap : ProductMapBase<ProductVariant>
    {
        public ProductVariantMap()
        {
            References(p => p.Master, "ProductMaster_Id").Not.Nullable();
            Map(p => p.Color).CustomType<Color>();
            HasMany(p => p.SaleProducts).Cascade.AllDeleteOrphan();
        }
    }
}
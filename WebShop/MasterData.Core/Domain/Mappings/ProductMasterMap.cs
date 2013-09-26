using MasterData.Core.Domain.MasterData;

namespace MasterData.Core.Domain.Mappings
{
    public class ProductMasterMap : ProductMapBase<ProductMaster>
    {
        public ProductMasterMap()
        {
            Map(p => p.ProductNumber);
            HasMany(p => p.Variants).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
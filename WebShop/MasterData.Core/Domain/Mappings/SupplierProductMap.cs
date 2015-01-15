using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Mappings
{
    public class SupplierProductMap : MasterDataObjectMap<SupplierProduct>
    {
        public SupplierProductMap()
        {
            References(p => p.Supplier).Not.Nullable();
            References(p => p.Variant, "ProductVariant_Id").Not.Nullable();
            HasMany(p => p.StoreProducts).Cascade.AllDeleteOrphan().Inverse();
            Map(p => p.CurrentStockCount);
            Map(p => p.CostPrice);
        }
    }
}
using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Mappings
{
    public class StoreProductMap : ProductMapBase<StoreProduct>
    {
        public StoreProductMap()
        {
            References(p => p.Reseller).Not.Nullable();
            References(s => s.SupplierProduct, "SupplierProduct_Id").Not.Nullable();
        }
    }
}
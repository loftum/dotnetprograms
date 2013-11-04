using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Mappings
{
    public class ProductTypeMap : MasterDataObjectMap<ProductType>
    {
        public ProductTypeMap()
        {
            Map(t => t.Name).Not.Nullable().Unique();
            Map(t => t.VatRate);
            HasMany(t => t.ProductMasters).ReadOnly().LazyLoad().Cascade.None();
        }
    }
}
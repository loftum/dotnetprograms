using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Mappings
{
    public abstract class ProductMapBase<TProduct> : MasterDataObjectMap<TProduct> where TProduct : Product
    {
        protected ProductMapBase()
        {
            Map(p => p.Name);
            Map(p => p.Description);
        }
    }
}
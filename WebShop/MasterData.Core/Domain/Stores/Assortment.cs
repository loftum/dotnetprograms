using System.Collections.Generic;
using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Stores
{
    public class Assortment
    {
        public virtual IList<StoreProduct> StoreProducts { get; protected set; }

        public Assortment(IList<StoreProduct> storeProducts)
        {
            StoreProducts = storeProducts;
        }
    }
}
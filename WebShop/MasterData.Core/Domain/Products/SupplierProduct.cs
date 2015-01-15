using System.Collections.Generic;
using MasterData.Core.Domain.Stores;

namespace MasterData.Core.Domain.Products
{
    public class SupplierProduct : MasterDataObject
    {
        public virtual Supplier Supplier { get; set; }
        public virtual ProductVariant Variant { get; set; }
        public virtual decimal CostPrice { get; set; }

        public virtual IList<StoreProduct> StoreProducts { get; set; }

        public virtual int CurrentStockCount { get; set; }

        protected SupplierProduct()
        {
            StoreProducts = new List<StoreProduct>();
        }

        public SupplierProduct(ProductVariant variant) : this()
        {
            Variant = variant;
        }
    }
}
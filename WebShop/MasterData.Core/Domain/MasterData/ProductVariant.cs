using System;
using System.Collections.Generic;

namespace MasterData.Core.Domain.MasterData
{
    public class ProductVariant : Product
    {
        public override Product Parent
        {
            get { return Master; }
        }

        public virtual ProductMaster Master { get; set; }
        public override string Name
        {
            get { return string.Format("{0} ({1})", FromMaster(m => m.Name), Color); }
            set {}
        }
        public virtual Color Color { get; set; }
        public virtual IList<SaleProduct> SaleProducts { get; set; }

        public ProductVariant()
        {
            SaleProducts = new List<SaleProduct>();
        }

        private T FromMaster<T>(Func<ProductMaster, T> property, T defaultValue = default(T))
        {
            return Master == null ? defaultValue : property(Master);
        }


        public virtual void Add(SaleProduct saleProduct)
        {
            SaleProducts.Add(saleProduct);
            saleProduct.Variant = this;
        }
    }
}
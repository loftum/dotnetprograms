using System.Collections.Generic;
using MasterData.Core.Domain.Stores;

namespace MasterData.Core.Domain.Products
{
    public class ProductMaster : Product
    {
        public virtual ProductType ProductType { get; set; }
        public virtual Producer Producer { get; set; }
        public override Product Parent { get { return null; } }
        public virtual IList<ProductVariant> Variants { get; set; }

        public ProductMaster()
        {
            Variants = new List<ProductVariant>();
        }

        public virtual void Add(ProductVariant variant)
        {
            Variants.Add(variant);
            variant.Master = this;
        }
    }
}
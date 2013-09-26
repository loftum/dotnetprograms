using System.Collections.Generic;

namespace MasterData.Core.Domain.MasterData
{
    public class ProductMaster : Product
    {
        public override Product Parent { get { return null; } }
        public virtual string ProductNumber { get; set; }
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
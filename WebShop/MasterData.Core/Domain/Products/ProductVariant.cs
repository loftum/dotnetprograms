using System.Collections.Generic;

namespace MasterData.Core.Domain.Products
{
    public class ProductVariant : Product
    {
        public override Product Parent
        {
            get { return Master; }
        }

        public virtual ProductMaster Master { get; set; }

        public virtual string ProductNumber { get; set; }

        public virtual Color Color { get; set; }
        public virtual IList<SupplierProduct> SupplierProducts { get; set; } 

        public ProductVariant()
        {
            SupplierProducts = new List<SupplierProduct>();
            Name = GetDefaultName();
        }

        private string GetDefaultName()
        {
            var name = Master == null ? "New variant" : Master.Name;
            return string.Format("{0} {1}", name, Color);
        }

        public ProductVariant(ProductMaster master) : this()
        {
            Master = master;
        }

        public virtual void Add(SupplierProduct supplierProduct)
        {
            SupplierProducts.Add(supplierProduct);
            supplierProduct.Variant = this;
        }
    }
}
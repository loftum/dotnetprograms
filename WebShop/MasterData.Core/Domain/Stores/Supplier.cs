using System.Collections.Generic;
using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Stores
{
    public class Supplier : MasterDataObject
    {
        public virtual string Name { get; set; }
        public virtual IList<Reseller> Resellers { get; set; }
        public virtual IList<SupplierProduct> Products { get; set; }

        public Supplier()
        {
            Resellers = new List<Reseller>();
            Products = new List<SupplierProduct>();
        }

        public virtual void Add(SupplierProduct product)
        {
            Products.Add(product);
            product.Supplier = this;
            foreach (var reseller in Resellers)
            {
                reseller.ThisProductIsAvailable(product);
            }
        }

        public virtual void Remove(SupplierProduct product)
        {
            Products.Remove(product);
            product.Supplier = null;
            foreach (var reseller in Resellers)
            {
                reseller.ThisProductIsNoLongerAvailable(product);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Stores
{
    public class Reseller : Store
    {
        public override Store Parent { get { return null; } }
        public virtual IList<Salespoint> Salespoints { get; set; }
        public virtual IList<Supplier> Suppliers { get; set; }
        public virtual IList<StoreProduct> StoreProducts { get; set; }

        public override IEnumerable<StoreProduct> GetAvailableStoreProducts()
        {
            return SaleProductFilter.Filter(StoreProducts);
        }

        public Reseller()
        {
            Salespoints = new List<Salespoint>();
            Suppliers = new List<Supplier>();
            StoreProducts = new List<StoreProduct>();
        }

        public virtual void ThisProductIsAvailable(SupplierProduct product)
        {
            StoreProducts.Add(new StoreProduct(product){Reseller = this});
        }

        public virtual void ThisProductIsNoLongerAvailable(SupplierProduct product)
        {
            foreach (var storeProduct in StoreProducts.RemoveWhere(s => s.SupplierProduct == product))
            {
                storeProduct.PrepareToDie();
            }
        }

        public virtual void AddSupplier(Supplier supplier)
        {
            Suppliers.Add(supplier);
        }

        public virtual void AddSalespoint(Salespoint salespoint)
        {
            Salespoints.Add(salespoint);
            salespoint.Reseller = this;
        }

        public virtual void RemoveSupplier(Supplier supplier)
        {
            Suppliers.Remove(supplier);
        }
    }
}
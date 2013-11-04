using MasterData.Core.Domain.Pricing;
using MasterData.Core.Domain.Stores;

namespace MasterData.Core.Domain.Products
{
    public class StoreProduct : Product
    {
        public virtual Reseller Reseller { get; set; }
        public override Product Parent { get { return Variant; } }
        public virtual ProductVariant Variant { get { return SupplierProduct.Variant; } }
        public virtual SupplierProduct SupplierProduct { get; set; }
        public virtual PriceCalculation PriceCalculation { get; set; }

        protected StoreProduct()
        {
        }

        public StoreProduct(SupplierProduct supplierProduct)
        {
            SupplierProduct = supplierProduct;
        }

        public virtual string ProductNumber
        {
            get { return Variant.ProductNumber; }
        }

        public virtual Price CostPrice
        {
            get { return Price.FromExVat(SupplierProduct.CostPrice, Variant.Master.ProductType.VatRate); }
        }

        public virtual void PrepareToDie()
        {
            Reseller = null;
            SupplierProduct = null;
        }
    }
}
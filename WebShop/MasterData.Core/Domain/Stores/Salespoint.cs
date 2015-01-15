using System.Collections.Generic;
using System.Linq;
using MasterData.Core.Domain.Pricing;
using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Stores
{
    public class Salespoint : Store
    {
        public override Store Parent { get { return Reseller; } }
        public virtual Reseller Reseller { get; set; }
        public virtual string Identifier { get; set; }
        public virtual IList<SaleProduct> SaleProducts { get; set; }

        public override IEnumerable<StoreProduct> GetAvailableStoreProducts()
        {
            return HasParent
                ? SaleProductFilter.Filter(Parent.GetAvailableStoreProducts())
                : Enumerable.Empty<StoreProduct>();
        }

        public Salespoint()
        {
            SaleProducts = new List<SaleProduct>();
        }

        public virtual void RecalculateSaleproducts()
        {
            ClearSaleProducts();
            foreach (var saleProduct in CalculateSaleProducts())
            {
                SaleProducts.Add(saleProduct);
            }
        }

        public virtual void ClearSaleProducts()
        {
            foreach (var saleProduct in SaleProducts)
            {
                saleProduct.PrepareToDie();
            }
            SaleProducts.Clear();
        }

        public virtual IEnumerable<SaleProduct> CalculateSaleProducts()
        {
            return GetAvailableStoreProducts()
                .Select(p => new SaleProduct
                    {
                        Salespoint = this,
                        Name = p.GetName(),
                        Description = p.GetDescription(),
                        ProductNumber = p.ProductNumber,
                        Price = CalculatePriceFor(p),
                        SearchableText = p.GetName()
                    });
        }

        private Price CalculatePriceFor(StoreProduct storeProduct)
        {
            return GetPriceCalculationFor(storeProduct).Calculate(storeProduct.CostPrice);
        }

        private PriceCalculation GetPriceCalculationFor(StoreProduct storeProduct)
        {
            return storeProduct.PriceCalculation ?? GetPriceCalculation();
        }
    }
}
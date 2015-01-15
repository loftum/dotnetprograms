using WebShop.Common.Models.Pricing;
using WebShop.Core.Model;

namespace WebShop.Core.Users
{
    public class BasketItemModel
    {
        public int Number { get; set; }
        public PriceModel Price { get { return Product.Price; } }
        public WebShopProductModel Product { get; set; }

        public BasketItemModel(WebShopProductModel product)
        {
            Product = product;
        }
    }
}
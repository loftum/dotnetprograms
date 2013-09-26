using WebShop.Common.Models.Pricing;

namespace WebShop.Core.Model
{
    public class WebShopProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PriceModel Price { get; set; }

        public WebShopProductModel()
        {
            Price = new PriceModel(0m, 0m);
        }
    }
}
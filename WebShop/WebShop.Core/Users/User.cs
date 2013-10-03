using System.Linq;
using WebShop.Core.Model;

namespace WebShop.Core.Users
{
    public class User
    {
        public Basket Basket { get; set; }

        public bool ShowPricesIncVat { get; set; }

        public User()
        {
            Basket = new Basket();
            ShowPricesIncVat = true;
        }

        public void ClearBasket()
        {
            Basket = new Basket();
        }

        public void AddToBasket(WebShopProductModel product)
        {
            Basket.Add(new BasketItem(product));
        }

        public void RemoveBasketItem(int lineNumber)
        {
            var item = Basket.Items.SingleOrDefault(i => i.Number == lineNumber);
            if (item == null)
            {
                return;
            }
            Basket.Remove(item);
        }
    }
}
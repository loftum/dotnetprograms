using System.Linq;
using WebShop.Core.Model;

namespace WebShop.Core.Users
{
    public class UserModel
    {
        public BasketModel Basket { get; set; }

        public bool ShowPricesIncVat { get; set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName
        {
            get { return string.Join(" ", FirstName, LastName); }
        }

        public UserModel(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            ClearBasket();
            ShowPricesIncVat = true;
        }

        public UserModel() : this("", "")
        {
        }

        public void ClearBasket()
        {
            Basket = new BasketModel();
            Basket.Personalia.FirstName = FirstName;
            Basket.Personalia.LastName = LastName;
        }

        public void AddToBasket(WebShopProductModel product)
        {
            Basket.Add(new BasketItemModel(product));
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
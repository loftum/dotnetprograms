using System.Collections.Generic;
using System.Linq;
using WebShop.Common.ExtensionMethods;
using WebShop.Common.Models.Pricing;

namespace WebShop.Core.Users
{
    public class Basket
    {
        public bool IsEmpty { get { return !Items.Any(); } }
        public List<BasketItem> Items { get; set; }
        public PriceModel Total { get { return Items.Sum(i => i.Price); } }
        public Personalia Personalia { get; set; }
        public Payment Payment { get; set; }

        public void Add(BasketItem item)
        {
            Items.Add(item);
            ReIndexItems();
        }

        public void Remove(BasketItem item)
        {
            Items.Remove(item);
            ReIndexItems();
        }

        private void ReIndexItems()
        {
            for (var ii = 0; ii < Items.Count; ii++)
            {
                Items[ii].Number = ii;
            }
        }

        public Basket()
        {
            Items = new List<BasketItem>();
            Personalia = new Personalia();
            Payment = new Payment();
        }
    }
}
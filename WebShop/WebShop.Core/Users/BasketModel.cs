using System.Collections.Generic;
using System.Linq;
using WebShop.Common.ExtensionMethods;
using WebShop.Common.Models.Pricing;

namespace WebShop.Core.Users
{
    public class BasketModel
    {
        public bool IsEmpty { get { return !Items.Any(); } }
        public List<BasketItemModel> Items { get; set; }
        public PriceModel Total { get { return Items.Sum(i => i.Price); } }
        public PersonaliaModel Personalia { get; set; }
        public PaymentModel Payment { get; set; }

        public void Add(BasketItemModel item)
        {
            Items.Add(item);
            ReIndexItems();
        }

        public void Remove(BasketItemModel item)
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

        public BasketModel()
        {
            Items = new List<BasketItemModel>();
            Personalia = new PersonaliaModel();
            Payment = new PaymentModel();
        }
    }
}
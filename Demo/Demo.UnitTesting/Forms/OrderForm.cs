using System.Collections.Generic;

namespace Demo.UnitTesting.Forms
{
    public class OrderForm
    {
        public string OrderId { get; set; }
        public List<OrderItem> Items { get; set; }

        public OrderForm()
        {
            Items = new List<OrderItem>();
        }
    }
}
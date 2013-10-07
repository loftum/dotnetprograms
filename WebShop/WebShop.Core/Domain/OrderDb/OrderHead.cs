using System.Collections.Generic;

namespace WebShop.Core.Domain.OrderDb
{
    public class OrderHead : OrderDbObjectWithChangeStamp
    {
        public long OrderNumber { get; set; }
        public Buyer Buyer { get; set; }
        public IList<OrderLine> Lines { get; set; }

        public OrderHead()
        {
            Buyer = new Buyer();
            Lines = new List<OrderLine>();
        }
    }
}
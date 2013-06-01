using System.Collections.Generic;

namespace DotNetPrograms.Common.UnitTests.Common.Mapping
{
    public class Order
    {
        public int OrderId { get; set; }
        public long? Number { get; set; }
        public Customer Customer { get; set; }
        public IList<OrderLine> Lines { get; set; }

        public Order()
        {
            Lines = new List<OrderLine>();
        }
    }
}
using System.Collections.Generic;

namespace DotNetPrograms.Common.UnitTests.Common.Mapping
{
    public class Order
    {
        public int OrderId { get; set; }
        public long? Number { get; set; }
        public Customer Customer { get; set; }
        public IList<OrderLine> Lines { get; set; }
        public IDictionary<string, int> Dictionary { get; set; }

        public Order()
        {
            Lines = new List<OrderLine>();
            Dictionary = new Dictionary<string, int>();
        }
    }
}
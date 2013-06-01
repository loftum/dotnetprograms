using System.Collections.Generic;

namespace DotNetPrograms.Common.UnitTests.Common.Mapping
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public long? Number { get; set; }
        public CustomerDto Customer { get; set; }
        public List<OrderLineDto> Lines { get; set; }

        public OrderDto()
        {
            Lines = new List<OrderLineDto>();
        }
    }
}
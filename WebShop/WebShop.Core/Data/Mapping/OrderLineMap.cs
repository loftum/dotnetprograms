using System.Data.Entity;
using WebShop.Core.Domain.OrderDb;

namespace WebShop.Core.Data.Mapping
{
    public class OrderLineMap : OrderDbObjectMap<OrderLine>
    {
        public OrderLineMap(DbModelBuilder builder) : base(builder)
        {
            Map.Property(l => l.ProductNumber);
            Map.Property(l => l.ProductName);
        }
    }
}
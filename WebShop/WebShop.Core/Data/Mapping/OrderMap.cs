using System.Data.Entity;
using WebShop.Core.Domain.OrderDb;

namespace WebShop.Core.Data.Mapping
{
    public class OrderMap : OrderDbObjectMap<OrderHead>
    {
        public OrderMap(DbModelBuilder builder) : base(builder)
        {
            Map.Property(o => o.OrderNumber);
            Map.HasMany(o => o.Lines).WithRequired(l => l.OrderHead);
        }
    }
}
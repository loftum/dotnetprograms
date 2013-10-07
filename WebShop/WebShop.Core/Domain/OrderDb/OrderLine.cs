using MasterData.Core.Domain.Pricing;

namespace WebShop.Core.Domain.OrderDb
{
    public class OrderLine : OrderDbObjectWithChangeStamp
    {
        public OrderHead OrderHead { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public Price Price { get; set; }
    }
}
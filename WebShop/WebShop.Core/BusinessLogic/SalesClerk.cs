using WebShop.Common.ExtensionMethods;
using WebShop.Core.Data;
using WebShop.Core.Domain.OrderDb;
using WebShop.Core.Users;

namespace WebShop.Core.BusinessLogic
{
    public class SalesClerk : ISalesClerk
    {
        private readonly IOrderRepository _repo;

        public SalesClerk(IOrderRepository repo)
        {
            _repo = repo;
        }

        public OrderHead Expedite(BasketModel basket)
        {
            var order = basket.MapTo<OrderHead>();
            _repo.Save(order);
            _repo.Commit();
            return order;
        }
    }
}
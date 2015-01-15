using WebShop.Core.Domain.OrderDb;
using WebShop.Core.Users;

namespace WebShop.Core.BusinessLogic
{
    public interface ISalesClerk
    {
        OrderHead Expedite(BasketModel basket);
    }
}
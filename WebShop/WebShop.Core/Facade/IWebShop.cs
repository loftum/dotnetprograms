using System;
using WebShop.Core.Users;

namespace WebShop.Core.Facade
{
    public interface IWebShop
    {
        void AddToBasket(Guid saleProductId);
        User User { get; }
    }
}
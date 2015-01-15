using System;
using WebShop.Core.Model;
using WebShop.Core.Users;

namespace WebShop.Core.Facade
{
    public interface IWebShop
    {
        void AddToBasket(Guid saleProductId);
        UserModel User { get; }
        ReceiptModel FulfilPurchase();
    }
}
using System;
using WebShop.Common.ExtensionMethods;
using WebShop.Core.BusinessLogic;
using WebShop.Core.Model;
using WebShop.Core.Users;

namespace WebShop.Core.Facade
{
    public class WebShop : IWebShop
    {
        private readonly IUserSession _userSession;
        private readonly IProductFacade _productFacade;
        private readonly ISalesClerk _salesClerk;

        public UserModel User { get { return _userSession.User; } }

        public WebShop(IUserSession userSession,
            IProductFacade productFacade,
            ISalesClerk salesClerk)
        {
            _userSession = userSession;
            _productFacade = productFacade;
            _salesClerk = salesClerk;
        }

        public void AddToBasket(Guid saleProductId)
        {
            var product = _productFacade.GetProduct(saleProductId);
            User.AddToBasket(product);
        }

        public ReceiptModel FulfilPurchase()
        {
            var order = _salesClerk.Expedite(User.Basket);
            User.ClearBasket();
            return order.MapTo<ReceiptModel>();
        }
    }
}
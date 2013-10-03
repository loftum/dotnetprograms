using System;
using WebShop.Core.Users;

namespace WebShop.Core.Facade
{
    public class WebShop : IWebShop
    {
        private readonly IUserSession _userSession;
        private readonly IProductFacade _productFacade;

        public User User { get { return _userSession.User; } }

        public WebShop(IUserSession userSession,
            IProductFacade productFacade)
        {
            _userSession = userSession;
            _productFacade = productFacade;
        }

        public void AddToBasket(Guid saleProductId)
        {
            var product = _productFacade.GetProduct(saleProductId);
            User.AddToBasket(product);
        }
    }
}
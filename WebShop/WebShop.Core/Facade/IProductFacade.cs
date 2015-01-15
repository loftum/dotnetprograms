using System;
using DotNetPrograms.Common.Paging;
using WebShop.Core.Model;

namespace WebShop.Core.Facade
{
    public interface IProductFacade
    {
        PagedList<WebShopProductModel> GetProducts(string searchText, int pageNumber, int pageSize);
        WebShopProductModel GetProduct(Guid id);
    }
}
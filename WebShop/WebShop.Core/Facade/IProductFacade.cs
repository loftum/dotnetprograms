using DotNetPrograms.Common.Paging;
using WebShop.Core.Model;

namespace WebShop.Core.Facade
{
    public interface IProductFacade
    {
        PagedList<ProductModel> GetProducts(int pageNumber, int pageSize);
    }
}
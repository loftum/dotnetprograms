using System.Linq;
using DotNetPrograms.Common.Paging;
using WebShop.Core.Domain.MasterData;
using WebShop.Common.ExtensionMethods;

namespace WebShop.Core.Model
{
    public class PagedProductList : PagedList<ProductModel>
    {
        public PagedProductList(IQueryable<Product> items, int pageNumber, int pageSize)
        {
            Initialize(items, p => p.MapTo<ProductModel>(), pageNumber, pageSize);
        }
    }
}
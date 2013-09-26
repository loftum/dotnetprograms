using System.Linq;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Domain.MasterData;
using WebShop.Common.ExtensionMethods;

namespace WebShop.Core.Model
{
    public class PagedSaleProductList : PagedList<WebShopProductModel>
    {
        public SearchInput SearchInput { get; private set; }

        public PagedSaleProductList(IQueryable<SaleProduct> items, int pageNumber, int pageSize)
        {
            SearchInput = new SearchInput();
            Initialize(items, p => p.MapTo<WebShopProductModel>(), pageNumber, pageSize);
        }
    }
}
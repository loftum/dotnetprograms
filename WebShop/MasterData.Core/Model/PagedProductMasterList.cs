using System.Linq;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Domain.MasterData;
using WebShop.Common.ExtensionMethods;

namespace MasterData.Core.Model
{
    public class PagedProductMasterList : PagedList<ProductMasterModel>
    {
        public PagedProductMasterList(IQueryable<ProductMaster> items, int pageNumber, int pageSize)
        {
            Initialize(items, p => p.MapTo<ProductMasterModel>(), pageNumber, pageSize);
        }
    }
}
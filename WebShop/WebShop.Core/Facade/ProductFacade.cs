using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Data;
using MasterData.Core.Domain.MasterData;
using WebShop.Core.Model;

namespace WebShop.Core.Facade
{
    public class ProductFacade : IProductFacade
    {
        private readonly IMasterDataRepository _repo;

        public ProductFacade(IMasterDataRepository repo)
        {
            _repo = repo;
        }

        public PagedList<WebShopProductModel> GetProducts(string searchText, int pageNumber, int pageSize)
        {
            var saleProducts = _repo.GetAll<SaleProduct>();

            if (!searchText.IsNullOrEmpty())
            {
                saleProducts = saleProducts.Where(sp =>
                    sp.Name.Contains(searchText) ||
                    sp.Variant.Name.Contains(searchText) ||
                    sp.Variant.Master.Name.Contains(searchText)
                    );
            }

            return new PagedSaleProductList(saleProducts, pageNumber, pageSize);
        }
    }
}
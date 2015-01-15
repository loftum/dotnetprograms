using System;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Data;
using MasterData.Core.Domain.Products;
using MasterData.Core.Domain.Stores;
using WebShop.Common.Configuration;
using WebShop.Common.ExtensionMethods;
using WebShop.Core.Model;

namespace WebShop.Core.Facade
{
    public class ProductFacade : IProductFacade
    {
        private readonly IMasterDataRepository _repo;
        private readonly IConfigSettings _settings;

        public ProductFacade(IMasterDataRepository repo,
            IConfigSettings settings)
        {
            _repo = repo;
            _settings = settings;
        }

        public PagedList<WebShopProductModel> GetProducts(string searchText, int pageNumber, int pageSize)
        {
            var salespoint = _repo
                .GetAll<Salespoint>()
                .Single(sp => sp.Identifier == _settings.SalespointIdentifier);

            var saleProducts = salespoint.SaleProducts
                .Where(sp => searchText.IsNullOrEmpty() || sp.SearchableText.Contains(searchText));

            return new PagedSaleProductList(saleProducts, pageNumber, pageSize);
        }

        public WebShopProductModel GetProduct(Guid id)
        {
            var saleProduct = _repo.GetOrThrow<StoreProduct>(id);
            return saleProduct.MapTo<WebShopProductModel>();
        }
    }
}
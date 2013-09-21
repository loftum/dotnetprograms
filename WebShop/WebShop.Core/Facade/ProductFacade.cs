using DotNetPrograms.Common.Paging;
using WebShop.Core.Data;
using WebShop.Core.Domain.MasterData;
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

        public PagedList<ProductModel> GetProducts(int pageNumber, int pageSize)
        {
            return new PagedProductList(_repo.GetAll<Product>(), pageNumber, pageSize);
            
        }
    }
}
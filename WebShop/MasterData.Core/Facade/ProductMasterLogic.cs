using MasterData.Core.Data;
using MasterData.Core.Domain.MasterData;
using MasterData.Core.Model;

namespace MasterData.Core.Facade
{
    public class ProductMasterLogic : IProductMasterLogic
    {
        private readonly IMasterDataRepository _repo;

        public ProductMasterLogic(IMasterDataRepository repo)
        {
            _repo = repo;
        }

        public PagedProductMasterList GetProducts()
        {
            return new PagedProductMasterList(_repo.GetAll<ProductMaster>(), 1, 20);
        }
    }
}
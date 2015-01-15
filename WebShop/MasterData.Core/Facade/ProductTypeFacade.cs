using System;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Data;
using MasterData.Core.Domain.Products;
using MasterData.Core.Model;
using MasterData.Core.Model.Products;
using WebShop.Common.ExtensionMethods;

namespace MasterData.Core.Facade
{
    public class ProductTypeFacade : IProductTypeFacade
    {
        private readonly IMasterDataRepository _repo;

        public ProductTypeFacade(IMasterDataRepository repo)
        {
            _repo = repo;
        }

        public PagedList<ProductTypeModel> GetProductTypes(string searchText, int pageNumber, int pageSize)
        {
            return _repo.GetAll<ProductType>()
                .Where(p => string.IsNullOrEmpty(searchText) || p.Name.Contains(searchText))
                .Paged(pageNumber, pageSize, t => t.MapTo<ProductTypeModel>());
        }

        public ProductTypeModel Edit(Guid id)
        {
            return id.IsDefault() ? new ProductTypeModel() : _repo.GetOrThrow<ProductType>(id).MapTo<ProductTypeModel>();
        }

        public IObjectIdentifier Save(ProductTypeModel input)
        {
            var type = input.IsNew ? new ProductType() : _repo.GetOrThrow<ProductType>(input.Id);
            type.Name = input.Name;
            type.VatRate = input.VatRate;
            _repo.Save(type);
            _repo.Commit();
            return new ObjectIdentifier(input.Name);
        }
    }
}
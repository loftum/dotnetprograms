using System;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Model;
using MasterData.Core.Model.Products;

namespace MasterData.Core.Facade
{
    public interface IProductTypeFacade
    {
        PagedList<ProductTypeModel> GetProductTypes(string searchText, int pageNumber, int pageSize);
        ProductTypeModel Edit(Guid id);
        IObjectIdentifier Save(ProductTypeModel input);
    }
}
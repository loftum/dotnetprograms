using System;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Model;
using MasterData.Core.Model.Products;

namespace MasterData.Core.Facade
{
    public interface IProductFacade
    {
        IPagedList<ProductMasterModel> GetProducts(string searchText, int pageNumber, int pageSize);
        EditProductMasterModel EditMaster(Guid id);
        IObjectIdentifier Save(EditProductMasterModel input);
        EditProductVariantModel EditVariant(Guid id);
        IObjectIdentifier Save(EditProductVariantModel input);
        EditProductVariantModel NewVariantForMaster(Guid id);
    }
}
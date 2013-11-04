using System;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Model;
using MasterData.Core.Model.Suppliers;

namespace MasterData.Core.Facade
{
    public interface ISupplierFacade
    {
        IPagedList<SupplierModel> GetSuppliers(string searchText, int pageNumber, int pageSize);
        EditSupplierModel Edit(Guid id);
        IObjectIdentifier Save(EditSupplierModel input);
    }
}
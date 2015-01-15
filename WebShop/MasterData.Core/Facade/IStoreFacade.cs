using System;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Model;
using MasterData.Core.Model.Stores;

namespace MasterData.Core.Facade
{
    public interface IStoreFacade
    {
        IPagedList<ResellerModel> SearchResellers(string searchText, int pageNumber, int pageSize);
        EditResellerModel EditReseller(Guid id);
        IObjectIdentifier Save(EditResellerModel input);
        EditSalespointModel EditSalespoint(Guid id);
        EditSalespointModel NewForReseller(Guid id);
        IObjectIdentifier Save(EditSalespointModel model);
        IPagedList<SalespointModel> SearchSalespoints(string searchText, int pageNumber, int pageSize);
        void GenerateSaleProductsForSalespoint(Guid id);
    }
}
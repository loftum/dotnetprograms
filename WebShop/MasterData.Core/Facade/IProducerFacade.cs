using System;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Model;
using MasterData.Core.Model.Products;

namespace MasterData.Core.Facade
{
    public interface IProducerFacade
    {
        PagedList<ProducerModel> GetProducers(string searchText, int pageNumber, int pageSize);
        ProducerModel Get(Guid id);
        IObjectIdentifier Save(ProducerModel input);
    }
}
using System;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Data;
using MasterData.Core.Domain.Stores;
using MasterData.Core.Model;
using MasterData.Core.Model.Products;
using WebShop.Common.ExtensionMethods;

namespace MasterData.Core.Facade
{
    public class ProducerFacade : IProducerFacade
    {
        private readonly IMasterDataRepository _repo;

        public ProducerFacade(IMasterDataRepository repo)
        {
            _repo = repo;
        }

        public PagedList<ProducerModel> GetProducers(string searchText, int pageNumber, int pageSize)
        {
            return _repo.GetAll<Producer>()
                .Where(p => string.IsNullOrEmpty(searchText) || p.Name.Contains(searchText))
                .Paged(pageNumber, pageSize, p => p.MapTo<ProducerModel>());
        }

        public ProducerModel Get(Guid id)
        {
            return id.IsDefault() ? new ProducerModel() : _repo.GetOrThrow<Producer>(id).MapTo<ProducerModel>();
        }

        public IObjectIdentifier Save(ProducerModel input)
        {
            var producer = input.IsNew ? new Producer() : _repo.GetOrThrow<Producer>(input.Id);
            producer.Name = input.Name;
            _repo.Save(producer);
            _repo.Commit();
            return new ObjectIdentifier(producer.Name);
        }
    }
}
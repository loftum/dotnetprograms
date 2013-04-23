using System.Linq;
using BasicManifest.Core.Domain;
using BasicManifest.Data.Repositories;
using BasicManifest.Lib.ExtensionMethods;
using BasicManifest.Lib.Models;

namespace BasicManifest.Lib.Facades
{
    public class CampFacade : ICampFacade
    {
        private readonly IBMRepo _repo;

        public CampFacade(IBMRepo repo)
        {
            _repo = repo;
        }

        public CampIndexModel GetCamps()
        {
            return new CampIndexModel(_repo.GetAll<Camp>().Select(c => c.MapTo<CampModel>()));
        }

        public CampModel Edit(long id)
        {
            var camp = _repo.GetOrThrow<Camp>(id);
            return camp.MapTo<CampModel>();
        }
    }
}
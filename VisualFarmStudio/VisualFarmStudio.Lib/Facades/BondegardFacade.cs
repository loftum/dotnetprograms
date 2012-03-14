using System.Collections.Generic;
using System.Linq;
using VisualFarmStudio.Core.Domain;
using VisualFarmStudio.Core.Repository;
using VisualFarmStudio.Lib.Caching;

namespace VisualFarmStudio.Lib.Facades
{
    public class BondegardFacade : IBondegardFacade
    {
        private readonly IVisualFarmRepo _repo;
        private readonly ICacheManager _cacheManager;

        public BondegardFacade(IVisualFarmRepo repo,
            ICacheManager cacheManager)
        {
            _repo = repo;
            _cacheManager = cacheManager;
        }

        public Bondegard GetDefaultBondegard()
        {
            return GetAllBondegards().FirstOrDefault();
        }

        public IEnumerable<Bondegard> GetAllBondegards()
        {
            return _cacheManager.GetAllBondegards(() => _repo.GetAll<Bondegard>());
        }

        public void Save(Bondegard bondegard)
        {
            _repo.Save(bondegard);
        }
    }
}
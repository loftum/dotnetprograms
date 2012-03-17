using System.Collections.Generic;
using System.Linq;
using VisualFarmStudio.Core.Domain;
using VisualFarmStudio.Core.Repository;
using VisualFarmStudio.Lib.Caching;
using VisualFarmStudio.Lib.Containers;
using VisualFarmStudio.Lib.Model;
using VisualFarmStudio.Lib.UnitOfWork;

namespace VisualFarmStudio.Lib.Facades
{
    public class BondegardFacade : IBondegardFacade
    {
        private readonly IVisualFarmRepo _repo;
        private readonly ICacheManager _cacheManager;
        private readonly IUnitOfWork _unitOfWork;

        public BondegardFacade(IVisualFarmRepo repo,
            ICacheManager cacheManager,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _cacheManager = cacheManager;
            _unitOfWork = unitOfWork;
        }

        public BondegardModel GetDefaultBondegard()
        {
            return GetAllBondegards().FirstOrDefault();
        }

        public IEnumerable<BondegardModel> GetAllBondegards()
        {
            return GetBondegardContainer().Bondegards;
        }

        private BondegardContainer GetBondegardContainer()
        {
            return _cacheManager.GetAllBondegards(LoadBondegards);
        }

        private BondegardContainer LoadBondegards()
        {
            return new BondegardContainer(_repo.GetAll<Bondegard>().Select(b => new BondegardModel(b)).ToList());
        }

        public void Save(BondegardModel bondegard)
        {
            using(var work = _unitOfWork.Begin())
            {
                var saved = _repo.Save(bondegard.ToEntity());
                GetBondegardContainer().Bondegards.Add(new BondegardModel(saved));
                work.Complete();
            }
        }
    }
}
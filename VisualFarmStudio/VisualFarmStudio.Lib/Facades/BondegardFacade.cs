using System;
using System.Collections.Generic;
using System.Linq;
using VisualFarmStudio.Common.Configuration;
using VisualFarmStudio.Common.Exceptions;
using VisualFarmStudio.Common.ExtensionMethods;
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
        private readonly IVFSConfig _config;
        private readonly Random _random = new Random();

        public BondegardFacade(IVisualFarmRepo repo,
            ICacheManager cacheManager,
            IUnitOfWork unitOfWork, IVFSConfig config)
        {
            _repo = repo;
            _cacheManager = cacheManager;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public BondegardModel GetDefaultBondegard()
        {
            return GetAllBondegards().FirstOrDefault();
        }

        public BondegardModel GetBondegard(long id)
        {
            var bondegard = GetAllBondegards().FirstOrDefault(b => b.Id == id);
            if (bondegard == null)
            {
                throw new UserException(ExceptionType.UnknownBondegard, id);
            }
            return bondegard;
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
                if (_config.Behave == "Like a clown")
                {
                    throw new Kake();
                }
                var existing = _repo.Get<Bondegard>(bondegard.Id);
                if (existing == null)
                {
                    throw new UserException(ExceptionType.UnknownBondegard, bondegard.Id);
                }
                existing.Navn = bondegard.Navn;
                _repo.Save(existing);
                work.Complete();
            }
        }

        public void GenerateBondegards(BondeModel bondeModel, int number)
        {
            using (var work = _unitOfWork.Begin())
            {
                var bonde = _repo.Get<Bonde>(bondeModel.Id);
                for (var ii = 1; ii < number + 1; ii++)
                {
                    var bondegard = new Bondegard {Navn = string.Format("Bondegård {0}", ii), Bonde = bonde};
                    _repo.Save(bondegard);
                    CreateTraktorer(3).Each(bondegard.AddTraktor);
                    CreateFjoser(3).Each(bondegard.AddFjos);
                    CreateStaller(2).Each(bondegard.AddStall);
                }
                work.Complete();
            }
        }


        private IList<Fjos> CreateFjoser(int number)
        {
            var fjoser = new List<Fjos>();
            
            for (var ii = 1; ii < number + 1; ii++)
            {
                var fjos = new Fjos();
                CreateKuer(8).Each(fjos.AddKu);
                fjoser.Add(fjos);
            }
            return fjoser;
        }

        private IList<Ku> CreateKuer(int number)
        {
            var kuer = new List<Ku>();
            for (var ii = 1; ii < number + 1; ii++)
            {
                kuer.Add(new Ku { Navn = RandomOf("Dagros", "Klara ku", "Litago", "Stjerna", "Betty", "Rødlin", "Dokka", "Hjertros", "Trine", "Sara", "Pia", "Dolly") });
            }
            return kuer;
        }

        private IList<Stall> CreateStaller(int number)
        {
            var staller = new List<Stall>();
            for (var ii = 1; ii < number + 1; ii++)
            {
                var stall = new Stall();
                CreateHester(10).Each(stall.AddHest);
                staller.Add(stall);
            }
            return staller;
        }

        private IList<Hest> CreateHester(int number)
        {
            var hester = new List<Hest>();
            for (var ii = 1; ii < number + 1; ii++)
            {
                hester.Add(new Hest { Navn = RandomOf("Blakken", "Jolly Jumper", "Flassfisbruna", "Silkesvarten", "Pølsa", "Lynet", "Dovregutten", "Happy Hepp", "Baltica", "Gregg", "Glory Story", "Farma", "Huldregubben") });
            }
            return hester;
        }

        private IList<Traktor> CreateTraktorer(int number)
        {
            var traktorer = new List<Traktor>();
            for (var ii = 1; ii < number + 1; ii++)
            {
                traktorer.Add(new Traktor
                {
                    Merke = RandomOf("Ford", "Massey Fergusson", "John Deere", "Volvo", "Valvet", "Tor")
                });
            }
            return traktorer;
        }

        private T RandomOf<T>(params T[] values)
        {
            return values[_random.Next(0, values.Length)];
        }
    }
}
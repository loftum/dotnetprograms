using System.Linq;
using VisualFarmStudio.Common.Exceptions;
using VisualFarmStudio.Common.Validation;
using VisualFarmStudio.Core.Domain;
using VisualFarmStudio.Core.Repository;
using VisualFarmStudio.Lib.Model;
using VisualFarmStudio.Lib.UnitOfWork;
using VisualFarmStudio.Lib.UserSession;

namespace VisualFarmStudio.Lib.Facades
{
    public class BondeFacade : IBondeFacade
    {
        private readonly IVisualFarmRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        public BondeFacade(IVisualFarmRepo repo,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public bool IsTaken(string brukernavn)
        {
            var lowercase = brukernavn.ToLowerInvariant();
            return _repo.GetAll<Bonde>().Any(b => b.Brukernavn.Equals(lowercase));
        }

        public BondeModel Get(string brukernavn)
        {
            new InputValidator().Require(() => brukernavn).OrThrow();

            var lowercase = brukernavn.ToLowerInvariant();
            var bonde = _repo.GetAll<Bonde>().SingleOrDefault(b => b.Brukernavn.Equals(lowercase));
            if (bonde == null)
            {
                throw new UserException(ExceptionType.InvalidCredentials);
            }
            return new BondeModel(bonde);
        }

        public void Add(BondeModel model)
        {
            if (IsTaken(model.Brukernavn))
            {
                throw new UserException(ExceptionType.BrukernavnIsTaken);
            }

            using (var work = _unitOfWork.Begin())
            {
                var bonde = _repo.Save(model.ToEntity());
                var bruker = _repo.GetAll<Rolle>().Single(r => r.Kode.Equals(UserRole.Bruker));
                bonde.AddRolle(bruker);
                work.Complete();
            }
        }
    }
}
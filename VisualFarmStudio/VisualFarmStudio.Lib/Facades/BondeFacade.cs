using System.Linq;
using VisualFarmStudio.Common.Exceptions;
using VisualFarmStudio.Core.Domain;
using VisualFarmStudio.Core.Repository;
using VisualFarmStudio.Lib.Model;
using VisualFarmStudio.Lib.UserSession;

namespace VisualFarmStudio.Lib.Facades
{
    public class BondeFacade : IBondeFacade
    {
        private readonly IVisualFarmRepo _repo;

        public BondeFacade(IVisualFarmRepo repo)
        {
            _repo = repo;
        }

        public bool IsTaken(string brukernavn)
        {
            var lowercase = brukernavn.ToLowerInvariant();
            return _repo.GetAll<Bonde>().Any(b => b.Brukernavn.Equals(lowercase));
        }

        public BondeModel Get(string brukernavn)
        {
            var lowercase = brukernavn.ToLowerInvariant();
            var bonde = _repo.GetAll<Bonde>().FirstOrDefault(b => b.Brukernavn.Equals(lowercase));
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

            var brukerRolle = _repo.GetAll<Rolle>().Single(r => r.Kode.Equals(UserRole.Bruker));
            model.Rolles.Add(new RolleModel(brukerRolle));
            _repo.Save(model.ToEntity());
        }
    }
}
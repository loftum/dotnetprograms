using System.Collections.Generic;
using System.Linq;
using VisualFarmStudio.Common.ExtensionMethods;
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Model
{
    public class BondeModel : BaseModel<Bonde>
    {
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Brukernavn { get; set; }
        public IList<RolleModel> Rolles { get; set; }

        public BondeModel(Bonde bonde) : base(bonde)
        {
            Fornavn = bonde.Fornavn;
            Etternavn = bonde.Etternavn;
            Brukernavn = bonde.Brukernavn.ToLowerInvariant();
            Rolles = bonde.Rolles.Select(r => new RolleModel(r)).ToList();
        }

        public BondeModel()
        {
            Rolles = new List<RolleModel>();
        }

        protected override Bonde MapTo(Bonde bonde)
        {
            bonde.Fornavn = Fornavn;
            bonde.Etternavn = Etternavn;
            bonde.Brukernavn = Brukernavn.ToLowerInvariant();
            Rolles.Each(r => bonde.AddRolle(r.ToEntity()));
            return bonde;
        }
    }
}
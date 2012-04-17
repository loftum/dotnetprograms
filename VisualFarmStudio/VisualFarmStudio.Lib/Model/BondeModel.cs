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
        public string FulltNavn { get { return string.Format("{0} {1}", Fornavn, Etternavn); } }
        
        private string _brukernavn;
        public string Brukernavn
        {
            get { return _brukernavn; }
            set { _brukernavn = value.ToLowerInvariant(); }
        }
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
            bonde.Brukernavn = Brukernavn;
            Rolles.Each(r => bonde.AddRolle(r.ToEntity()));
            return bonde;
        }
    }
}
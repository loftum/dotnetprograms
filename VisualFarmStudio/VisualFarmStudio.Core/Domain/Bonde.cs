using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Bonde : DomainObject
    {
        public virtual string Fornavn { get; set; }
        public virtual string Etternavn { get; set; }
        public virtual string Brukernavn { get; set; }

        public virtual IList<Bondegard> Bondegards { get; set; }
        public virtual IList<Rolle> Rolles { get; set; }

        public Bonde()
        {
            Bondegards = new List<Bondegard>();
            Rolles = new List<Rolle>();
        }

        public virtual void AddRolle(Rolle rolle)
        {
            Rolles.Add(rolle);
        }
    }
}
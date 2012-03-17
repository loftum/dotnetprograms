using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Fjos : DomainObject
    {
        public virtual IList<Ku> Kuer { get; set; }

        public Fjos()
        {
            Kuer = new List<Ku>();
        }

        public virtual void AddKu(Ku ku)
        {
            Kuer.Add(ku);
        }
    }
}
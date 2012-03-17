using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Fjos : DomainObject
    {
        public virtual IList<Ku> Kues { get; set; }

        public Fjos()
        {
            Kues = new List<Ku>();
        }

        public virtual void AddKu(Ku ku)
        {
            Kues.Add(ku);
        }
    }
}
using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Bondegard : DomainObject
    {
        public virtual string Navn { get; set; }
        public virtual IList<Fjos> Fjoses { get; set; }
        public virtual IList<Stall> Stalls { get; set; }
        public virtual IList<Traktor> Traktors { get; set; }

        public Bondegard()
        {
            Stalls = new List<Stall>();
            Fjoses = new List<Fjos>();
            Traktors = new List<Traktor>();
        }

        public virtual void AddStall(Stall stall)
        {
            Stalls.Add(stall);
        }

        public virtual void AddFjos(Fjos fjos)
        {
            Fjoses.Add(fjos);
        }

        public virtual void AddTraktor(Traktor traktor)
        {
            Traktors.Add(traktor);
        }
    }
}
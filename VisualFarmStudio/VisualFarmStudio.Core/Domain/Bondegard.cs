using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Bondegard : DomainObject
    {
        public virtual string Navn { get; set; }
        public virtual IList<Fjos> Fjoser { get; set; }
        public virtual IList<Stall> Staller { get; set; }
        public virtual IList<Traktor> Traktorer { get; set; }

        public Bondegard()
        {
            Staller = new List<Stall>();
            Fjoser = new List<Fjos>();
            Traktorer = new List<Traktor>();
        }

        public virtual void AddStall(Stall stall)
        {
            Staller.Add(stall);
        }

        public virtual void AddFjos(Fjos fjos)
        {
            Fjoser.Add(fjos);
        }

        public virtual void AddTraktor(Traktor traktor)
        {
            Traktorer.Add(traktor);
        }
    }
}
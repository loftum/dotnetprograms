using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Bondegard : DomainObject
    {
        public virtual string Navn { get; set; }
        public virtual IList<Fjos> Fjoser { get; set; }
        public virtual IList<Stall> Staller { get; set; }
        public virtual IList<Traktor> Traktorer { get; set; }
    }
}
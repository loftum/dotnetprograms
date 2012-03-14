using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Stall : DomainObject
    {
        public virtual IList<Hest> Hester { get; set; }
    }
}
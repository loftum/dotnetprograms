using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Fjos : DomainObject
    {
        public virtual IList<Ku> Kuer { get; set; }
    }
}
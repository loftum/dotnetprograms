using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Stall : DomainObject
    {
        public virtual Bondegard Bondegard { get; set; }

        public virtual IList<Hest> Hestes { get; set; }

        public Stall()
        {
            Hestes = new List<Hest>();
        }

        public virtual void AddHest(Hest hest)
        {
            Hestes.Add(hest);
            hest.Stall = this;
        }
    }
}
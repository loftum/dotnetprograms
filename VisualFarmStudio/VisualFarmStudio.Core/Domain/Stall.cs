using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Stall : DomainObject
    {
        public virtual IList<Hest> Hester { get; set; }

        public Stall()
        {
            Hester = new List<Hest>();
        }

        public virtual void AddHest(Hest hest)
        {
            Hester.Add(hest);
        }
    }
}
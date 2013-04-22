using System.Collections.Generic;
using System.Linq;

namespace BasicManifest.Core.Domain
{
    public class Load : DomainObject
    {
        public virtual Day Day { get; set; }
        public virtual decimal DefaultSlotPrice { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<LoadGroup> Groups { get; protected set; }
        public virtual decimal Price { get { return Groups.Sum(s => s.Price); } }

        public Load()
        {
            Groups = new List<LoadGroup>();
        }

        public virtual void Add(LoadGroup loadGroup)
        {
            loadGroup.Load = this;
            Groups.Add(loadGroup);
        }

        public virtual void GetPaid()
        {
            foreach (var group in Groups)
            {
                group.GetPaid();
            }
        }

        public virtual Camp GetCamp()
        {
            return Day.Camp;
        }
    }
}
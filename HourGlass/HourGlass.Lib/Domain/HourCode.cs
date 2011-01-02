using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HourGlass.Lib.Domain
{
    public class HourCode : DomainObject
    {
        public virtual IList<HourUsage> Usages { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual bool InUse { get { return Usages.Count > 0; } }
        public virtual double Usage { get { return Usages.Sum(usage => usage.Sum); } }

        public HourCode()
        {
            Usages = new List<HourUsage>();
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Code).Append(": ").Append(Name).ToString();
        }
    }
}
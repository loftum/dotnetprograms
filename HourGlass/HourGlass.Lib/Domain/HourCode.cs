using System.Collections.Generic;
using System.Text;

namespace HourGlass.Lib.Domain
{
    public class HourCode : DomainObject
    {
        public virtual IList<HourUsage> Usages { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

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
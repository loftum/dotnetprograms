using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HourGlass.Lib.Domain
{
    public class Week : DomainObject
    {
        public virtual int Year { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual int Number { get { return StartDate.DayOfYear/7; } }

        public virtual IList<HourUsage> Usages { get; set; }

        public virtual double Monday { get { return Usages.Sum(usage => usage.Monday); } }
        public virtual double Tuesday { get { return Usages.Sum(usage => usage.Tuesday); } }
        public virtual double Wednesday { get { return Usages.Sum(usage => usage.Wednesday); } }
        public virtual double Thursday { get { return Usages.Sum(usage => usage.Thursday); } }
        public virtual double Friday { get { return Usages.Sum(usage => usage.Friday); } }
        public virtual double Saturday { get { return Usages.Sum(usage => usage.Saturday); } }
        public virtual double Sunday { get { return Usages.Sum(usage => usage.Sunday); } }
        
        public virtual double Sum
        {
            get { return Usages.Sum(usage => usage.Sum); }
        }

        public Week()
        {
            Usages = new List<HourUsage>();
        }

        public virtual void AddUsage(HourUsage usage)
        {
            usage.Week = this;
            Usages.Add(usage);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Year)
                .Append(" - ")
                .Append(Number).ToString();
        }
    }
}
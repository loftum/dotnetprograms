using System;
using System.Collections.Generic;
using DotNetPrograms.Common.DateAndTime;

namespace BasicManifest.Core.Domain
{
    public class Day : DomainObject
    {
        public virtual DateTime Date { get; set; }
        public virtual bool IsClosed { get; protected set; }
        public virtual Camp Camp { get; set; }
        public virtual IList<Load> Loads { get; protected set; }

        public Day()
        {
            Date = DateTimeProvider.ReasonableMinValue;
            Loads = new List<Load>();
        }

        public virtual void Add(Load load)
        {
            load.Day = this;
            load.DefaultSlotPrice = Camp.DefaultSlotPrice;
            Loads.Add(load);
        }

        public virtual void Close()
        {
            IsClosed = true;
        }
    }
}
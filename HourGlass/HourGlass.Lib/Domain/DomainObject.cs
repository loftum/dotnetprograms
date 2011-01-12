using System;

namespace HourGlass.Lib.Domain
{
    public abstract class DomainObject
    {
        public virtual long Id { get; set; }
        public virtual bool IsPersisted { get { return Id > 0; } }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
    }
}
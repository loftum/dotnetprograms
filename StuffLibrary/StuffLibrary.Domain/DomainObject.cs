using System;

namespace StuffLibrary.Domain
{
    public abstract class DomainObject
    {
        public virtual long Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime ModifiedAt { get; set; }
    }
}
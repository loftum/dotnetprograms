using System;
using DotNetPrograms.Common.DateAndTime;

namespace BasicManifest.Core.Domain
{
    public abstract class DomainObject : IAuditable
    {
        public virtual bool IsNew { get { return Id == default(long); } }
        public virtual long Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual string ModifiedBy { get; set; }

        protected DomainObject()
        {
            CreatedDate = DateTimeProvider.ReasonableMinValue;
            ModifiedDate = DateTimeProvider.ReasonableMinValue;
        }

        public virtual Type GetUnproxiedType()
        {
            return GetType();
        }

        public override bool Equals(object obj)
        {
            var other = obj as DomainObject;
            return other != null &&
                other.Id == Id &&
                other.GetUnproxiedType() == GetUnproxiedType();
        }

        public override int GetHashCode()
        {
            return string.Format("{0}{1}", GetUnproxiedType().FullName, Id).GetHashCode();
        }
    }
}
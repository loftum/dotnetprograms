using System;

namespace MasterData.Core.Domain
{
    public abstract class MasterDataObject
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsNew { get { return Id == default(Guid); } }
        public virtual Type GetUnproxiedType()
        {
            return GetType();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MasterDataObject);
        }

        public virtual bool Equals(MasterDataObject other)
        {
            return other != null &&
                other.GetUnproxiedType() == GetUnproxiedType() &&
                other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
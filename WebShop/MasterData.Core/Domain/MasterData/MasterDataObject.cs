using System;

namespace MasterData.Core.Domain.MasterData
{
    public abstract class MasterDataObject
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsNew { get { return Id == default(Guid); } }
        public virtual Type GetUnproxiedType()
        {
            return GetType();
        }
    }
}
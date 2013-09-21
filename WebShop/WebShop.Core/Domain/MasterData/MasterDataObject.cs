using System;

namespace WebShop.Core.Domain.MasterData
{
    public abstract class MasterDataObject
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsNew { get { return Id == default(Guid); } }
    }
}
using System;

namespace WebShop.Core.Domain.OrderDb
{
    public class OrderDbObject
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsNew { get { return Id == default(Guid); } }
    }
}
using System;

namespace WebShop.Core.Domain.Transactions
{
    public class TransactionsObject
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsNew { get { return Id == default(Guid); } } 
    }
}
using System.Collections.Generic;
using System.Linq;

namespace BasicManifest.Core.Domain
{
    public class Account : DomainObject
    {
        public virtual IList<Transaction> Transactions { get; protected set; }
        private decimal _balance;
        public virtual decimal Balance
        {
            get
            {
                _balance = Transactions.Sum(t => t.Amount);
                return _balance;
            }
            protected set { _balance = value; }
        }

        public Account()
        {
            Transactions = new List<Transaction>();
        }

        public virtual void Add(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public virtual void Insert(decimal amount, string description = null)
        {
            var desc = description ?? (amount < 0 ? "Withdrawal" : "Deposit");
            Add(new Transaction(amount, desc));
        }

        public virtual void Withdraw(decimal amount, string description = null)
        {
            Insert(-amount, description);
        }
    }
}
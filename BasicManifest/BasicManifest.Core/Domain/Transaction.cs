namespace BasicManifest.Core.Domain
{
    public class Transaction : DomainObject
    {
        public virtual Account Account { get; set; }
        public virtual string Description { get; protected set; }
        public virtual decimal Amount { get; protected set; }
        private TransactionType _type;
        public virtual TransactionType Type
        {
            get
            {
                _type = Amount < 0 ? TransactionType.Credit : TransactionType.Debit;
                return _type;
            }
            protected set { _type = value; }
        }

        protected Transaction()
        {
        }

        public Transaction(decimal amount)
        {
            Amount = amount;
        }

        public Transaction(decimal amount, string description)
        {
            Amount = amount;
            Description = description;
        }
    }
}
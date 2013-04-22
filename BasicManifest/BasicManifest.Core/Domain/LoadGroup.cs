using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;

namespace BasicManifest.Core.Domain
{
    public class LoadGroup : DomainObject
    {
        public virtual Load Load { get; set; }
        private decimal _price;
        public virtual decimal Price
        {
            get
            {
                _price = Slots.Sum(s => s.Price);
                return _price;
            }
            protected set { _price = value; }
        }
        
        public virtual IList<Slot> Slots { get; protected set; }

        public LoadGroup()
        {
            Slots = new List<Slot>();
        }

        public virtual Camp GetCamp()
        {
            return Load.GetCamp();
        }

        public virtual IEnumerable<ICanPay> GetPayers()
        {
            var payers = Slots.Where(s => s.JumperPays).ToList();
            if (payers.Any())
            {
                return payers.Select(s => s.Jumper);
            }
            return GetCamp().AsArray();
        }

        public virtual void GetPaid()
        {
            var payers = GetPayers().ToList();
            var amount = Price / payers.Count;
            foreach (var payer in payers)
            {
                payer.Account.Withdraw(amount, Load.Name);
            }
        }

        public virtual void Add(Slot slot)
        {
            slot.Price = Load.DefaultSlotPrice;
            Slots.Add(slot);
        }
    }
}
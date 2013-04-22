using System.Collections.Generic;

namespace BasicManifest.Core.Domain
{
    public class Camp : DomainObject, ICanPay
    {
        public virtual string Name { get; set; }
        public virtual decimal DefaultSlotPrice { get; set; }
        public virtual Account Account { get; protected set; }
        public virtual IList<Person> Participants { get; protected set; }
        public virtual IList<Day> Days { get; protected set; }

        public Camp()
        {
            Participants = new List<Person>();
            Account = new Account();
            Days = new List<Day>();
        }

        public virtual void Add(Person person)
        {
            Participants.Add(person);
        }

        public virtual void Add(Day day)
        {
            day.Camp = this;
            Days.Add(day);
        }

        public virtual void Close(Day day)
        {
            foreach (var load in day.Loads)
            {
                load.GetPaid();
            }
            day.Close();
        }
    }
}
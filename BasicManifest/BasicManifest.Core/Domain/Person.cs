using System;
using System.Collections.Generic;

namespace BasicManifest.Core.Domain
{
    public class Person : DomainObject, ICanPay
    {
        public virtual PersonRole Role { get; set; }
        public virtual bool IsInstructor { get { return Role == PersonRole.Instructor; } }
        public virtual bool IsParticipant { get { return Role == PersonRole.Participant; } }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual Account Account { get; set; }
        public virtual IList<Slot> Slots { get; protected set; }

        public Person()
        {
            Account = new Account();
            Slots = new List<Slot>();
        }
    }
}
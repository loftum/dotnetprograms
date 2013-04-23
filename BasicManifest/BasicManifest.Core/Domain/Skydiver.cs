using System;
using System.Collections.Generic;
using DotNetPrograms.Common.DateAndTime;

namespace BasicManifest.Core.Domain
{
    public class Skydiver : DomainObject, ICanPay
    {
        public virtual PersonRole Role { get; set; }
        public virtual bool IsInstructor { get { return Role == PersonRole.Instructor; } }
        public virtual bool IsParticipant { get { return Role == PersonRole.Participant; } }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime BirthDate { get; set; }
        
        public virtual Account Account { get; set; }
        public virtual IList<Slot> Slots { get; protected set; }
        public virtual Camp Camp { get; set; }

        public Skydiver()
        {
            Account = new Account();
            Slots = new List<Slot>();
            BirthDate = DateTimeProvider.ReasonableMinValue;
        }
    }
}
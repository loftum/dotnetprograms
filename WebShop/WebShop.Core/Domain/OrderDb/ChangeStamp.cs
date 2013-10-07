using System;
using DotNetPrograms.Common.DateAndTime;

namespace WebShop.Core.Domain.OrderDb
{
    public class ChangeStamp
    {
        public DateTime CreatedDate { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTime ModifiedDate { get; protected set; }
        public string ModifiedBy { get; protected set; }

        public ChangeStamp()
        {
            CreatedDate = DateTimeProvider.Now;
            ModifiedDate = DateTimeProvider.Now;
        }

        public void YouWereCreatedNowBy(string who)
        {
            var now = DateTimeProvider.Now;
            CreatedDate = now;
            CreatedBy = who;
            ModifiedDate = now;
            ModifiedBy = who;
        }

        public void YouWereModifiedNowBy(string who)
        {
            ModifiedDate = DateTimeProvider.Now;
            ModifiedBy = who;
        }
    }
}
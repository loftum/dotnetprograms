using System;

namespace Deploy.Lib.DateAndTime
{
    public class DateProvider : IDateProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
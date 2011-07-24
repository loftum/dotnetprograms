using System;

namespace StuffLibrary.Common.DateAndTime
{
    public class DateProvider : IDateProvider
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
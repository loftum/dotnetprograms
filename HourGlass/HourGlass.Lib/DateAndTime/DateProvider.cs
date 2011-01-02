using System;

namespace HourGlass.Lib.DateAndTime
{
    public class DateProvider : IDateProvider
    {
        public int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }

        public int GetCurrentWeekNumber()
        {
            return DateTime.Now.DayOfYear / 7 + 1;
        }
    }
}
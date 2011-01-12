using System;

namespace HourGlass.Lib.DateAndTime
{
    public class DateProvider : IDateProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime Today()
        {
            return DateTime.Today;
        }

        public int GetCurrentYear()
        {
            return Now().Year;
        }

        public int GetCurrentWeekNumber()
        {
            return Now().DayOfYear / 7 + 1;
        }

        public DateTime GetCurrentWeekStartDate()
        {
            var day = Today();
            while (day.DayOfWeek != DayOfWeek.Monday)
            {
                day = day.AddDays(-1);
            }
            return day;
        }
    }
}
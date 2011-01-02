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

        public DateTime GetCurrentWeekStartDate()
        {
            var day = DateTime.Now;
            while (day.DayOfWeek > DayOfWeek.Monday)
            {
                day = day.AddDays(-1);
            }
            return day;
        }
    }
}
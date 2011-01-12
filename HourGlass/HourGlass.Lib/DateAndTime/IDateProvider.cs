using System;

namespace HourGlass.Lib.DateAndTime
{
    public interface IDateProvider
    {
        int GetCurrentYear();
        int GetCurrentWeekNumber();
        DateTime GetCurrentWeekStartDate();
        DateTime Now();
        DateTime Today();
    }
}
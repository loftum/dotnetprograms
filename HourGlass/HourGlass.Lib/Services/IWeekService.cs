using System;
using System.Collections.Generic;
using HourGlass.Lib.Domain;

namespace HourGlass.Lib.Services
{
    public interface IWeekService
    {
        IEnumerable<Week> GetRecentWeeks();
        Week NewWeek(DateTime maxDate);
        Week Remove(Week week);
        Week Save(Week week);
    }
}
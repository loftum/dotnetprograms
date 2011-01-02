using System;
using System.Collections.Generic;
using System.Linq;
using HourGlass.Lib.Data;
using HourGlass.Lib.DateAndTime;
using HourGlass.Lib.Domain;

namespace HourGlass.Lib.Services
{
    public class WeekService : IWeekService
    {
        private readonly IHourGlassRepo _repo;
        private readonly IDateProvider _dateProvider;

        public WeekService(IHourGlassRepo repo, IDateProvider dateProvider)
        {
            _repo = repo;
            _dateProvider = dateProvider;
        }

        public IEnumerable<Week> GetRecentWeeks()
        {
            return _repo.GetAll<Week>()
                .OrderByDescending(week => week.StartDate)
                .Take(20);
        }

        public Week NewWeek(DateTime maxDate)
        {
            maxDate = new List<DateTime> {maxDate, GetMaxStartDate()}.Max();
            var startDate = _dateProvider.GetCurrentWeekStartDate();
            while(startDate <= maxDate)
            {
                startDate = startDate.AddDays(7);
            }

            var week = new Week {StartDate = startDate};
            return _repo.Save(week);
        }

        public Week Save(Week week)
        {
            return _repo.Save(week);
        }

        public Week Remove(Week week)
        {
            if (week.IsPersisted)
            {
                _repo.Delete(week);
            }
            return week;
        }

        private DateTime GetMaxStartDate()
        {
            var weeks = _repo.GetAll<Week>();
            if (weeks.Count() > 0)
            {
                return weeks.Max(week => week.StartDate);
            }
            return new DateTime();
        }
    }
}
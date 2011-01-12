using System;
using HourGlass.Lib.Domain;

namespace HourGlass.UnitTesting.Builders
{
    public class WeekBuilder : BuilderBase<WeekBuilder, Week>
    {
        public WeekBuilder(Week item) : base(item)
        {
        }

        public static WeekBuilder NewWeek()
        {
            var week = new Week();
            return new WeekBuilder(week);
        }

        public WeekBuilder WithStartDate(DateTime startDate)
        {
            Item.StartDate = startDate;
            return this;
        }

        public WeekBuilder WithUsage(HourUsageBuilder builder)
        {
            return WithUsage(builder.Item);
        }

        public WeekBuilder WithUsage(HourUsage usage)
        {
            Item.AddUsage(usage);
            return this;
        }
    }
}
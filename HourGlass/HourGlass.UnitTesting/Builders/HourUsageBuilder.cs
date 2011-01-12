using HourGlass.Lib.Domain;

namespace HourGlass.UnitTesting.Builders
{
    public class HourUsageBuilder : BuilderBase<HourUsageBuilder, HourUsage>
    {
        public HourUsageBuilder(HourUsage item) : base(item)
        {
        }

        public static HourUsageBuilder NewHourUsage()
        {
            var usage = new HourUsage();
            return new HourUsageBuilder(usage);
        }

        public HourUsageBuilder WithHourCode(HourCodeBuilder builder)
        {
            return WithHourCode(builder.Item);
        }

        public HourUsageBuilder WithHourCode(HourCode hourCode)
        {
            Item.SetHourCode(hourCode);
            return this;
        }

        public HourUsageBuilder WithMonday(double monday)
        {
            Item.Monday = monday;
            return this;
        }

        public HourUsageBuilder WithTuesday(double tuesday)
        {
            Item.Tuesday = tuesday;
            return this;
        }

        public HourUsageBuilder WithWednesday(double wednesday)
        {
            Item.Wednesday = wednesday;
            return this;
        }

        public HourUsageBuilder WithThursday(double thursday)
        {
            Item.Thursday = thursday;
            return this;
        }

        public HourUsageBuilder WithFriday(double friday)
        {
            Item.Friday = friday;
            return this;
        }

        public HourUsageBuilder WithSaturday(double saturday)
        {
            Item.Saturday = saturday;
            return this;
        }

        public HourUsageBuilder WithSunday(double sunday)
        {
            Item.Sunday = sunday;
            return this;
        }
    }
}
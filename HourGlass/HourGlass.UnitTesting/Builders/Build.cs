namespace HourGlass.UnitTesting.Builders
{
    public class Build
    {
        public static WeekBuilder NewWeek()
        {
            return WeekBuilder.NewWeek();
        }

        public static HourCodeBuilder NewHourCode()
        {
            return HourCodeBuilder.NewHourCode();
        }

        public static HourUsageBuilder NewHourUsage()
        {
            return HourUsageBuilder.NewHourUsage();
        }
    }
}
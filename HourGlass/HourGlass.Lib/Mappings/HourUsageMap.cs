using HourGlass.Lib.Domain;

namespace HourGlass.Lib.Mappings
{
    public class HourUsageMap : DomainObjectMap<HourUsage>
    {
        public HourUsageMap()
        {
            References(x => x.Week);
            Map(x => x.Monday);
            Map(x => x.Tuesday);
            Map(x => x.Wednesday);
            Map(x => x.Thursday);
            Map(x => x.Friday);
            Map(x => x.Saturday);
            Map(x => x.Sunday);
            
        }
    }
}
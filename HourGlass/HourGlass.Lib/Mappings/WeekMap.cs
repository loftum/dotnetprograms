using HourGlass.Lib.Domain;

namespace HourGlass.Lib.Mappings
{
    public class WeekMap : DomainObjectMap<Week>
    {
        public WeekMap()
        {
            HasMany(x => x.Usages).Cascade.AllDeleteOrphan();
            Map(x => x.Year);
            Map(x => x.Number);
        }
    }
}
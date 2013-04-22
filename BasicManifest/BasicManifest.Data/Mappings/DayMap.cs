using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Mappings
{
    public class DayMap : DomainObjectMap<Day>
    {
        public DayMap()
        {
            Map(d => d.Date);
            Map(d => d.IsClosed);
            HasMany(d => d.Loads).Cascade.AllDeleteOrphan();
            References(d => d.Camp).Cascade.None();
        }
    }
}
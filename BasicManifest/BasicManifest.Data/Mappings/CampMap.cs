using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Mappings
{
    public class CampMap : DomainObjectMap<Camp>
    {
        public CampMap()
        {
            Map(c => c.Name);
            Map(c => c.DefaultSlotPrice);
            HasMany(c => c.Skydivers).Cascade.AllDeleteOrphan();
            HasMany(c => c.Days).Cascade.AllDeleteOrphan();
            References(c => c.Account).Cascade.All();
        }
    }
}
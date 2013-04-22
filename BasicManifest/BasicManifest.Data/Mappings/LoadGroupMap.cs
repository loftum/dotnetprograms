using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Mappings
{
    public class LoadGroupMap : DomainObjectMap<LoadGroup>
    {
        public LoadGroupMap()
        {
            Map(g => g.Price);
            References(g => g.Load);
            HasMany(g => g.Slots).Cascade.AllDeleteOrphan();
        }
    }
}
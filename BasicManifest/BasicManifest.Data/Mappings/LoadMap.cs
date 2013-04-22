using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Mappings
{
    public class LoadMap : DomainObjectMap<Load>
    {
        public LoadMap()
        {
            Map(l => l.Name);
            Map(l => l.DefaultSlotPrice);
            HasMany(l => l.Groups).Cascade.AllDeleteOrphan();
        }
    }
}
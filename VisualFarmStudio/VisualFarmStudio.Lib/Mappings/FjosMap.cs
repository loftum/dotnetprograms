using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Mappings
{
    public class FjosMap : DomainObjectMap<Fjos>
    {
        public FjosMap()
        {
            HasMany(f => f.Kuer).Cascade.All();
        }
    }
}
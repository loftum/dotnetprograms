using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class BondegardMap : DomainObjectMap<Bondegard>
    {
        public BondegardMap()
        {
            Map(g => g.Navn);
            HasMany(g => g.Fjoses).Cascade.SaveUpdate();
            HasMany(g => g.Stalls).Cascade.SaveUpdate();
            HasMany(g => g.Traktors).Cascade.SaveUpdate();
        }
    }
}
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Mappings
{
    public class BondegardMap : DomainObjectMap<Bondegard>
    {
        public BondegardMap()
        {
            Map(g => g.Navn);
            HasMany(g => g.Fjoser).Cascade.SaveUpdate();
            HasMany(g => g.Staller).Cascade.SaveUpdate();
            HasMany(g => g.Traktorer).Cascade.SaveUpdate();
        }
    }
}
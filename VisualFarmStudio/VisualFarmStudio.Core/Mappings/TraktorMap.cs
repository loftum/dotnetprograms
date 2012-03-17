using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class TraktorMap : DomainObjectMap<Traktor>
    {
        public TraktorMap()
        {
            Map(t => t.Merke);
            HasOne(t => t.Bondegard);
        }
    }
}
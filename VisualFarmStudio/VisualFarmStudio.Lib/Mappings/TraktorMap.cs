using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Mappings
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
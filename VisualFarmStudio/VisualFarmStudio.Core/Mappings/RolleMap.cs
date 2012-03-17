using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class RolleMap : DomainObjectMap<Rolle>
    {
        public RolleMap()
        {
            Map(r => r.Kode);
            Map(r => r.Navn);
        }
    }
}
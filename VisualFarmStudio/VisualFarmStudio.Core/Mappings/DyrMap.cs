using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public abstract class DyrMap<TDyr> : DomainObjectMap<TDyr> where TDyr : Dyr
    {
        protected DyrMap()
        {
            Map(d => d.Navn);
        }
    }
}
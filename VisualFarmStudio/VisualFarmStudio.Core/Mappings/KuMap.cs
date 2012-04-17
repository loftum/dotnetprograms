using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class KuMap : DyrMap<Ku>
    {
        public KuMap()
        {
            References(k => k.Fjos);
        }
    }
}
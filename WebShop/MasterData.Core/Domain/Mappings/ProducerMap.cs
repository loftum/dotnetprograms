using MasterData.Core.Domain.Stores;

namespace MasterData.Core.Domain.Mappings
{
    public class ProducerMap : MasterDataObjectMap<Producer>
    {
        public ProducerMap()
        {
            Map(p => p.Name);
            HasMany(p => p.Products).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
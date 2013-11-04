using MasterData.Core.Domain.Stores;

namespace MasterData.Core.Domain.Mappings
{
    public class SalespointMap : MasterDataObjectMap<Salespoint>
    {
        public SalespointMap()
        {
            Map(s => s.Name);
            Map(s => s.Identifier).Unique();
            References(s => s.Reseller).Not.Nullable();
            HasMany(s => s.SaleProducts).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
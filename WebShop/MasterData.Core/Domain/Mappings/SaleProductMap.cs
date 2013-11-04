using FluentNHibernate.Mapping;
using MasterData.Core.Domain.Pricing;
using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Mappings
{
    public class SaleProductMap : MasterDataObjectMap<SaleProduct>
    {
        public SaleProductMap()
        {
            References(p => p.Salespoint).Not.Nullable();
            Map(p => p.Name);
            Map(p => p.Description);
            Map(p => p.ProductNumber);
            Map(p => p.SearchableText);
            Component(p => p.Price, MapPrice);
        }

        private static void MapPrice(ComponentPart<Price> m)
        {
            m.Map(p => p.IncVat, "PriceIncVat");
            m.Map(p => p.ExVat, "PriceExVat");
        }
    }
}
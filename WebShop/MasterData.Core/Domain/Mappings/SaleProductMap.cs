using FluentNHibernate.Mapping;
using MasterData.Core.Domain.MasterData;
using MasterData.Core.Domain.Pricing;

namespace MasterData.Core.Domain.Mappings
{
    public class SaleProductMap : ProductMapBase<SaleProduct>
    {
        public SaleProductMap()
        {
            References(s => s.Variant, "ProductVariant_Id").Not.Nullable();
            Component(s => s.BasePrice, MapPrice);
        }

        private static void MapPrice(ComponentPart<Price> m)
        {
            m.Map(p => p.IncVat, "BasePriceIncVat");
            m.Map(p => p.ExVat, "BasePriceExVat");
        }
    }
}
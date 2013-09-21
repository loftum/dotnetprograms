using FluentNHibernate.Mapping;

namespace WebShop.Core.Domain.MasterData.Mappings
{
    public class ProductMap : MasterDataObjectMap<Product>
    {
        public ProductMap()
        {
            Map(p => p.Name);
            Map(p => p.ProductNumber);
            MapInheritable(p => p.Description);
            Component(p => p.Price, MapPrice);
            References(p => p.Parent, "Parent_Id").Nullable();
            HasMany(p => p.Children).Cascade.AllDeleteOrphan().Inverse();
        }

        private static void MapPrice(ComponentPart<Price> m)
        {
            m.Map(p => p.IncVat, "PriceIncVat");
            m.Map(p => p.ExVat, "PriceExVat");
        }
    }
}
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using MasterData.Core.Domain.Pricing;
using WebShop.Core.Domain.OrderDb;

namespace WebShop.Core.Data.Mapping
{
    public class OrderDbMapper
    {
        private readonly DbModelBuilder _builder;

        public OrderDbMapper(DbModelBuilder builder)
        {
            _builder = builder;
        }

        public void Map()
        {
            MapChangeStamp(_builder.ComplexType<ChangeStamp>());
            MapBuyer(_builder.ComplexType<Buyer>());
            MapPrice(_builder.ComplexType<Price>());
            Configure(new OrderMap(_builder),
                new OrderLineMap(_builder));
        }

        private static void MapPrice(ComplexTypeConfiguration<Price> m)
        {
            m.Property(p => p.IncVat).HasColumnName("PriceIncVat");
            m.Property(p => p.ExVat).HasColumnName("PriceExVat");
        }

        private static void MapBuyer(ComplexTypeConfiguration<Buyer> m)
        {
            m.Property(b => b.FirstName).HasColumnName("BuyerFirstName");
            m.Property(b => b.LastName).HasColumnName("BuyerLastName");
            m.Property(b => b.Email).HasColumnName("BuyerEmail");
            m.Property(b => b.PhoneNumber).HasColumnName("BuyerPhoneNumber");
        }

        private static void MapChangeStamp(ComplexTypeConfiguration<ChangeStamp> m)
        {
            m.Property(d => d.CreatedDate).HasColumnName("CreatedDate");
            m.Property(d => d.CreatedBy).HasColumnName("CreatedBy");
            m.Property(d => d.ModifiedDate).HasColumnName("ModifiedDate");
            m.Property(d => d.ModifiedBy).HasColumnName("ModifiedBy");
        }


        private static void Configure(params IOrderDbObjectMap[] maps)
        {
        }
    }
}
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using WebShop.Core.Domain.OrderDb;

namespace WebShop.Core.Data.Mapping
{
    public abstract class OrderDbObjectMap<TObject> : IOrderDbObjectMap where TObject : OrderDbObject
    {
        protected EntityTypeConfiguration<TObject> Map { get; private set; }

        protected OrderDbObjectMap(DbModelBuilder builder)
        {
            Map = builder.Entity<TObject>();
            Map.ToTable(typeof(TObject).Name);
            Map.HasKey(o => o.Id);
            Map.Ignore(o => o.IsNew);
        }
    }
}
using System.Data;
using MigSharp;
using WebShop.Migrations.ExtensionMethods;

namespace WebShop.Migrations.Steps
{
    [MigrationExport(Tag = "Create table Product")]
    public class M_001_CreateProduct1 : IReversibleMigration
    {
        private const string Product = "Product";
        private const string ParentId = "Parent_Id";
        private const string ProductNumber = "ProductNumber";

        public void Up(IDatabase db)
        {
            db.CreateTable(Product)
              .WithId()
              .WithNullableColumn(ParentId, DbType.Guid)
              .WithNotNullableColumn(ProductNumber, DbType.AnsiString).OfSize(100)
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(500)
              .WithNullableColumn("Description", DbType.AnsiString).OfSize(2000)
              .WithNotNullableColumn("PriceIncVat", DbType.Decimal).OfSize(19, 2)
              .WithNotNullableColumn("PriceExVat", DbType.Decimal).OfSize(19, 2);

            var table = db.Tables[Product];
            table.AddForeignKeyTo(Product).Through(ParentId, "Id");
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[Product];
            table.ForeignKeyTo(Product).Drop();
            table.Drop();
        }
    }
}
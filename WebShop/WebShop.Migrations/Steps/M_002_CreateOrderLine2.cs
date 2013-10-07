using System.Data;
using MigSharp;
using WebShop.Migrations.ExtensionMethods;

namespace WebShop.Migrations.Steps
{
    [MigrationExport(Tag = "Create table OrderLine")]
    public class M_002_CreateOrderLine2 : IReversibleMigration
    {
        private const string OrderLine = "OrderLine";
        private const string OrderHead = "OrderHead";

        public void Up(IDatabase db)
        {
            db.CreateTable(OrderLine)
              .WithId()
              .WithForeignKeyColumnTo(OrderHead)
              .WithNotNullableColumn("ProductNumber", DbType.AnsiString).OfSize(256)
              .WithNotNullableColumn("ProductName", DbType.AnsiString).OfSize(256)
              .WithNotNullableColumn("PriceIncVat", DbType.Decimal).OfSize(19,2)
              .WithNotNullableColumn("PriceExVat", DbType.Decimal).OfSize(19, 2)
              .WithChangeStamp();

            db.Tables[OrderLine].AddDefaultForeignKeyTo(OrderHead);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[OrderLine];
            table.ForeignKeyTo(OrderHead).Drop();
            table.Drop();
        }
    }
}
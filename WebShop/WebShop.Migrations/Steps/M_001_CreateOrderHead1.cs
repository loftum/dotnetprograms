using System.Data;
using MigSharp;
using WebShop.Migrations.ExtensionMethods;

namespace WebShop.Migrations.Steps
{
    [MigrationExport(Tag = "Create table Order")]
    public class M_001_CreateOrderHead1 : IReversibleMigration
    {
        private const string OrderHead = "OrderHead";

        public void Up(IDatabase db)
        {
            db.CreateTable(OrderHead)
              .WithId()
              .WithNotNullableColumn("OrderNumber", DbType.Int64).Unique()
              .WithNotNullableColumn("BuyerFirstName", DbType.AnsiString).OfSize(512)
              .WithNotNullableColumn("BuyerLastName", DbType.AnsiString).OfSize(512)
              .WithNotNullableColumn("BuyerEmail", DbType.AnsiString).OfSize(512)
              .WithNotNullableColumn("BuyerPhoneNumber", DbType.AnsiString).OfSize(15)
              .WithChangeStamp();
        }

        public void Down(IDatabase db)
        {
            db.Tables[OrderHead].Drop();
        }
    }
}
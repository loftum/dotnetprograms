using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table SaleProduct")]
    public class M_005_CreateSaleProduct5 : IReversibleMigration
    {
        private const string SaleProduct = "SaleProduct";
        private const string ProductVariant = "ProductVariant";

        public void Up(IDatabase db)
        {
            db.CreateTable(SaleProduct)
              .WithId()
              .WithForeignKeyColumnTo(ProductVariant)
              .WithNullableColumn("Name", DbType.AnsiString).OfSize(500)
              .WithNullableColumn("Description", DbType.AnsiString).OfSize(2000)
              .WithNotNullableColumn("BasePriceIncVat", DbType.Decimal).OfSize(19,2)
              .WithNotNullableColumn("BasePriceExVat", DbType.Decimal).OfSize(19,2);

            db.Tables[SaleProduct].AddDefaultForeignKeyTo(ProductVariant);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[SaleProduct];
            table.ForeignKeyTo(ProductVariant).Drop();
            table.Drop();
        }
    }
}
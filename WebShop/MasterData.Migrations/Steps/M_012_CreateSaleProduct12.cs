using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table SaleProduct")]
    public class M_012_CreateSaleProduct12 : IReversibleMigration
    {
        private const string SaleProduct = "SaleProduct";
        private const string Salespoint = "Salespoint";

        public void Up(IDatabase db)
        {
            db.CreateTable(SaleProduct)
              .WithId()
              .WithForeignKeyColumnTo(Salespoint)
              .WithNotNullableColumn("ProductNumber", DbType.AnsiString).OfSize(100)
              .WithNotNullableColumn("PriceIncVat", DbType.Decimal).OfSize(19, 2)
              .WithNotNullableColumn("PriceExVat", DbType.Decimal).OfSize(19, 2)
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(500)
              .WithNullableColumn("Description", DbType.AnsiString).OfSize(2000)
              .WithNullableColumn("SearchableText", DbType.AnsiString).OfSize(4000);
            db.Tables[SaleProduct].AddDefaultForeignKeyTo(Salespoint);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[SaleProduct];
            table.ForeignKeyTo(Salespoint).Drop();
            table.Drop();
        }
    }
}
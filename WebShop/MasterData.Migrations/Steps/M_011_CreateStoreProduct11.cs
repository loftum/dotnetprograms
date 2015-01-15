using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table StoreProduct")]
    public class M_011_CreateStoreProduct11 : IReversibleMigration
    {
        private const string StoreProduct = "StoreProduct";
        private const string Reseller = "Reseller";
        private const string SupplierProduct = "SupplierProduct";

        public void Up(IDatabase db)
        {
            db.CreateTable(StoreProduct)
              .WithId()
              .WithForeignKeyColumnTo(Reseller)
              .WithForeignKeyColumnTo(SupplierProduct)
              .WithNullableColumn("Name", DbType.AnsiString).OfSize(500)
              .WithNullableColumn("Description", DbType.AnsiString).OfSize(2000);

            var table = db.Tables[StoreProduct];
            table.AddDefaultForeignKeyTo(Reseller);
            table.AddDefaultForeignKeyTo(SupplierProduct);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[StoreProduct];
            table.ForeignKeyTo(Reseller).Drop();
            table.ForeignKeyTo(SupplierProduct).Drop();
            table.Drop();
        }
    }
}
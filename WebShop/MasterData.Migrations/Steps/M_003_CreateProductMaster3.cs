using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table ProductMaster")]
    public class M_003_CreateProductMaster3 : IReversibleMigration
    {
        private const string ProductMaster = "ProductMaster";
        private const string ProductNumber = "ProductNumber";

        public void Up(IDatabase db)
        {
            db.CreateTable(ProductMaster)
              .WithId()
              .WithNotNullableColumn(ProductNumber, DbType.AnsiString).OfSize(100)
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(500)
              .WithNullableColumn("Description", DbType.AnsiString).OfSize(2000);
        }

        public void Down(IDatabase db)
        {
            db.Tables[ProductMaster].Drop();
        }
    }
}
using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table ProductVariant")]
    public class M_004_CreateProductVariant4 : IReversibleMigration
    {
        private const string ProductVariant = "ProductVariant";
        private const string ProductMaster = "ProductMaster";

        public void Up(IDatabase db)
        {
            db.CreateTable(ProductVariant)
              .WithId()
              .WithForeignKeyColumnTo(ProductMaster)
              .WithNullableColumn("Name", DbType.AnsiString).OfSize(500)
              .WithNullableColumn("Description", DbType.AnsiString).OfSize(2000)
              .WithNotNullableColumn("Color", DbType.Int32);

            var table = db.Tables[ProductVariant];
            table.AddDefaultForeignKeyTo(ProductMaster);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[ProductVariant];
            table.ForeignKeyTo(ProductMaster).Drop();
            table.Drop();
        }
    }
}
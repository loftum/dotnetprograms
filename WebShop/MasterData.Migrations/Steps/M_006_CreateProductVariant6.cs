using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table ProductVariant")]
    public class M_006_CreateProductVariant6 : IReversibleMigration
    {
        private const string ProductVariant = "ProductVariant";
        private const string ProductMaster = "ProductMaster";
        private const string Color = "Color";
        private const string ProductNumber = "ProductNumber";

        public void Up(IDatabase db)
        {
            db.CreateTable(ProductVariant)
              .WithId()
              .WithForeignKeyColumnTo(ProductMaster)
              .WithForeignKeyColumnTo(Color)
              .WithNotNullableColumn(ProductNumber, DbType.AnsiString).OfSize(100).Unique()
              .WithNullableColumn("Name", DbType.AnsiString).OfSize(500)
              .WithNullableColumn("Description", DbType.AnsiString).OfSize(2000);

            var table = db.Tables[ProductVariant];
            table.AddDefaultForeignKeyTo(ProductMaster);
            table.AddDefaultForeignKeyTo(Color);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[ProductVariant];
            table.ForeignKeyTo(ProductMaster).Drop();
            table.ForeignKeyTo(Color).Drop();
            table.Drop();
        }
    }
}
using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table ProductMaster")]
    public class M_005_CreateProductMaster5 : IReversibleMigration
    {
        private const string ProductMaster = "ProductMaster";
        private const string Producer = "Producer";
        private const string ProductType = "ProductType";

        public void Up(IDatabase db)
        {
            db.CreateTable(ProductMaster)
              .WithId()
              .WithForeignKeyColumnTo(Producer)
              .WithForeignKeyColumnTo(ProductType)
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(500)
              .WithNullableColumn("Description", DbType.AnsiString).OfSize(2000);

            var table = db.Tables[ProductMaster];
            table.AddDefaultForeignKeyTo(Producer);
            table.AddDefaultForeignKeyTo(ProductType);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[ProductMaster];
            table.ForeignKeyTo(Producer).Drop();
            table.ForeignKeyTo(ProductType).Drop();
            table.Drop();
        }
    }
}
using System;
using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table ProductType")]
    public class M_003_CreateProductType3 : IReversibleMigration
    {
        private const string ProductType = "ProductType";

        public void Up(IDatabase db)
        {
            db.CreateTable(ProductType)
              .WithId()
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(200).Unique()
              .WithNotNullableColumn("VatRate", DbType.Decimal).OfSize(19, 2);
        }

        public void Down(IDatabase db)
        {
            db.Tables[ProductType].Drop();
        }
    }
}
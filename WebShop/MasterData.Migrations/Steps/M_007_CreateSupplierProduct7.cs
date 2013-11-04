using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table SupplierProduct")]
    public class M_007_CreateSupplierProduct7 : IReversibleMigration
    {
        private const string SupplierProduct = "SupplierProduct";
        private const string ProductVariant = "ProductVariant";
        private const string Supplier = "Supplier";
        private const string UniqueSupplierProduct = "UQ_SupplierProduct_Supplier_ProductVariant";

        public void Up(IDatabase db)
        {
            db.CreateTable(SupplierProduct)
              .WithId()
              .WithForeignKeyColumnTo(ProductVariant)
              .WithForeignKeyColumnTo(Supplier)
              .WithNotNullableColumn("CostPrice", DbType.Decimal).OfSize(19,2)
              .WithNotNullableColumn("CurrentStockCount", DbType.Int32);

            var table = db.Tables[SupplierProduct];
            table.AddDefaultForeignKeyTo(ProductVariant);
            table.AddDefaultForeignKeyTo(Supplier);
            table.AddUniqueConstraint(UniqueSupplierProduct)
                 .OnColumn(ForeignKey.ColumnTo(Supplier))
                 .OnColumn(ForeignKey.ColumnTo(ProductVariant));
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[SupplierProduct];
            table.UniqueConstraints[UniqueSupplierProduct].Drop();
            table.ForeignKeyTo(ProductVariant).Drop();
            table.ForeignKeyTo(Supplier).Drop();
            table.Drop();
        }
    }
}
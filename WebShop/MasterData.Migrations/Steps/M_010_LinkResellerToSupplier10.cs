using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Link Reseller to Supplier")]
    public class M_010_LinkResellerToSupplier10 : IReversibleMigration
    {
        private const string ResellerToSupplier = "ResellerToSupplier";
        private const string Reseller = "Reseller";
        private const string Supplier = "Supplier";
        private const string Unique = "UQ_ResellerToSupplier_Reseller_Supplier";

        public void Up(IDatabase db)
        {
            db.CreateTable(ResellerToSupplier)
              .WithForeignKeyColumnTo(Reseller)
              .WithForeignKeyColumnTo(Supplier);
            var table = db.Tables[ResellerToSupplier];
            table.AddDefaultForeignKeyTo(Reseller);
            table.AddDefaultForeignKeyTo(Supplier);
            table.AddUniqueConstraint(Unique)
                                         .OnColumn(ForeignKey.ColumnTo(Reseller))
                                         .OnColumn(ForeignKey.ColumnTo(Supplier));

        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[ResellerToSupplier];
            table.UniqueConstraints[Unique].Drop();
            table.ForeignKeyTo(Reseller).Drop();
            table.ForeignKeyTo(Supplier).Drop();
            table.Drop();
        }
    }
}
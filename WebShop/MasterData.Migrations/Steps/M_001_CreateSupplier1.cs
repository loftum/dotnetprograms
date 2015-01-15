using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table Supplier")]
    public class M_001_CreateSupplier1 : IReversibleMigration
    {
        private const string Supplier = "Supplier";

        public void Up(IDatabase db)
        {
            db.CreateTable(Supplier)
              .WithId()
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(500).Unique();
        }

        public void Down(IDatabase db)
        {
            db.Tables[Supplier].Drop();
        }
    }
}
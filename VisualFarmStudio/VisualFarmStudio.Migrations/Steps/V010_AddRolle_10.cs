using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag = "Add Rolle")]
    public class V010_AddRolle_10 : IReversibleMigration
    {
        private const string RolleTable = "Rolle";

        public void Up(IDatabase db)
        {
            db.CreateTable(RolleTable)
                .WithPrimaryKeyColumn("Id", DbType.Int64).AsIdentity()
                .WithNotNullableColumn("Kode", DbType.AnsiString).OfSize(255)
                .WithNotNullableColumn("Navn", DbType.AnsiString).OfSize(255);
            db.Tables[RolleTable].AddUniqueConstraint().OnColumn("Kode");
        }

        public void Down(IDatabase db)
        {
            db.Tables[RolleTable].UniqueConstraintOf("Kode").Drop();
            db.Tables[RolleTable].Drop();
        }
    }
}
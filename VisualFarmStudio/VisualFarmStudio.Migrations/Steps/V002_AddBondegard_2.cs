using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag = "Add Bondegard")]
    public class V002_AddBondegard_2 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.CreateTable("Bondegard")
                .WithPrimaryKeyColumn("Id", DbType.Int64).AsIdentity()
                .WithNotNullableColumn("Navn", DbType.AnsiString);
        }

        public void Down(IDatabase db)
        {
            db.Tables["Bondegard"].Drop();
        }
    }
}
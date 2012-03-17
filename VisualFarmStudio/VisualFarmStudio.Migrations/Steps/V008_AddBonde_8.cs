using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag = "Add Bonde")]
    public class V008_AddBonde_8 : IReversibleMigration
    {
        private const string BondeTable = "Bonde";

        public void Up(IDatabase db)
        {
            db.CreateTable(BondeTable)
                .WithPrimaryKeyColumn("Id", DbType.Int64).AsIdentity()
                .WithNotNullableColumn("Fornavn", DbType.AnsiString).OfSize(255)
                .WithNotNullableColumn("Etternavn", DbType.AnsiString).OfSize(255)
                .WithNotNullableColumn("Brukernavn", DbType.AnsiString).OfSize(255);
            db.Tables[BondeTable].AddUniqueConstraint().OnColumn("Brukernavn");
        }

        public void Down(IDatabase db)
        {
            db.Tables[BondeTable].UniqueConstraintOf("Brukernavn").Drop();
            db.Tables[BondeTable].Drop();
        }
    }
}
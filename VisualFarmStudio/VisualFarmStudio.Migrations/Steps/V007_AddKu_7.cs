using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag = "Add Ku")]
    public class V007_AddKu_7 : IReversibleMigration
    {
        private const string KuTable = "Ku";
        private const string FjosTable = "Fjos";

        public void Up(IDatabase db)
        {
            db.CreateTable(KuTable)
                .WithPrimaryKeyColumn("Id", DbType.Int64).AsIdentity()
                .WithNotNullableColumn("FjosId", DbType.Int64)
                .WithNotNullableColumn("Navn", DbType.AnsiString).OfSize(255);
            db.Tables[KuTable].AddForeignKeyTo(FjosTable).Through("FjosId", "Id");
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[KuTable];
            table.ForeignKeyTo(FjosTable).Drop();
            table.Drop();
        }
    }
}
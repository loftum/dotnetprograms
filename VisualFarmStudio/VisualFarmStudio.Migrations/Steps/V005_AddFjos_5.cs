using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag = "Add Fjos")]
    public class V005_AddFjos_5 : IReversibleMigration
    {
        private const string FjosTable = "Fjos";
        private const string BondegardTable = "Bondegard";

        public void Up(IDatabase db)
        {
            db.CreateTable(FjosTable)
                .WithPrimaryKeyColumn("Id", DbType.Int64).AsIdentity()
                .WithNotNullableColumn("BondegardId", DbType.Int64);
            db.Tables[FjosTable].AddForeignKeyTo(BondegardTable).Through("BondegardId", "Id");
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[FjosTable];
            table.ForeignKeyTo(BondegardTable).Drop();
            table.Drop();
        }
    }
}
using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag= "Add Traktor")]
    public class V003_AddTraktor_3 : IReversibleMigration
    {
        private const string TraktorTable = "Traktor";
        private const string BondegardTable = "Bondegard";

        public void Up(IDatabase db)
        {
            db.CreateTable(TraktorTable)
                .WithPrimaryKeyColumn("Id", DbType.Int64).AsIdentity()
                .WithNotNullableColumn("BondegardId", DbType.Int64)
                .WithNotNullableColumn("Merke", DbType.AnsiString).OfSize(255);
            db.Tables[TraktorTable].AddForeignKeyTo(BondegardTable).Through("BondegardId", "Id");
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[TraktorTable];
            table.ForeignKeyTo(BondegardTable).Drop();
            table.Drop();
        }
    }
}
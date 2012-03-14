using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag = "Add Stall")]
    public class V004_AddStall_4 : IReversibleMigration
    {
        private const string StallTable = "Stall";
        private const string BondegardTable = "Bondegard";

        public void Up(IDatabase db)
        {
            db.CreateTable(StallTable)
                .WithPrimaryKeyColumn("Id", DbType.Int64).AsIdentity()
                .WithNotNullableColumn("BondegardId", DbType.Int64);
            db.Tables[StallTable].AddForeignKeyTo(BondegardTable).Through("BondegardId", "Id");
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[StallTable];
            table.ForeignKeyTo(BondegardTable).Drop();
            table.Drop();
        }
    }
}
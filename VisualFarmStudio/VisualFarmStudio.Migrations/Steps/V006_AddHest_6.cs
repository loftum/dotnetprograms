using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag = "Add Hest")]
    public class V006_AddHest_6 : IReversibleMigration
    {
        private const string HestTable = "Hest";
        private const string StallTable = "Stall";

        public void Up(IDatabase db)
        {
            db.CreateTable(HestTable)
                .WithPrimaryKeyColumn("Id", DbType.Int64).AsIdentity()
                .WithNotNullableColumn("StallId", DbType.Int64);
            db.Tables[HestTable].AddForeignKeyTo(StallTable).Through("StallId", "Id");
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[HestTable];
            table.ForeignKeyTo(StallTable).Drop();
            table.Drop();
        }
    }
}
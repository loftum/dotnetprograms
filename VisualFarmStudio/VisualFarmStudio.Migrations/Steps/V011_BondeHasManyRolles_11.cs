using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag = "Bonde has many Rolles")]
    public class V011_BondeHasManyRolles_11 : IReversibleMigration
    {
        private const string BondeRolleTable = "BondeRolle";
        private const string BondeTable = "Bonde";
        private const string RolleTable = "Rolle";

        public void Up(IDatabase db)
        {
            db.CreateTable(BondeRolleTable)
                .WithPrimaryKeyColumn("Id", DbType.Int64).AsIdentity()
                .WithNotNullableColumn("BondeId", DbType.Int64)
                .WithNotNullableColumn("RolleId", DbType.Int64);
            var bondeRolle = db.Tables[BondeRolleTable];
            bondeRolle.AddForeignKeyTo(BondeTable).Through("BondeId", "Id");
            bondeRolle.AddForeignKeyTo(RolleTable).Through("RolleId", "Id");
        }

        public void Down(IDatabase db)
        {
            var bondeRolle = db.Tables[BondeRolleTable];
            bondeRolle.ForeignKeyTo(BondeTable).Drop();
            bondeRolle.ForeignKeyTo(RolleTable).Drop();
            bondeRolle.Drop();
        }
    }
}
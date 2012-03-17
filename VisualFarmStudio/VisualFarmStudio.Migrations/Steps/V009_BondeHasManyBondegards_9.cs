using System.Data;
using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport(Tag = "Bonde has many Bondegards")]
    public class V009_BondeHasManyBondegards_9 : IReversibleMigration
    {
        private const string BondeTable = "Bonde";
        private const string BondegardTable = "Bondegard";

        public void Up(IDatabase db)
        {
            var bondegard = db.Tables[BondegardTable];
            bondegard.AddNotNullableColumn("BondeId", DbType.Int64);
            bondegard.AddForeignKeyTo(BondeTable).Through("BondeId", "Id");
        }

        public void Down(IDatabase db)
        {
            var bondegard = db.Tables[BondegardTable];
            bondegard.ForeignKeyTo(BondeTable).Drop();
            bondegard.Columns["BondeId"].Drop();
        }
    }
}
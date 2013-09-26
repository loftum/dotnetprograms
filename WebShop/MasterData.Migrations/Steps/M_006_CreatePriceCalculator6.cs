using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table PriceCalculator")]
    public class M_006_CreatePriceCalculator6 : IReversibleMigration
    {
        private const string PriceCalculator = "PriceCalculator";

        public void Up(IDatabase db)
        {
            db.CreateTable(PriceCalculator)
              .WithId()
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(512);
        }

        public void Down(IDatabase db)
        {
            db.Tables[PriceCalculator].Drop();
        }
    }
}
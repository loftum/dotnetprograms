using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table PriceCalculation")]
    public class M_007_CreateTable_PriceCalculation7 : IReversibleMigration
    {
        private const string PriceCalculation = "PriceCalculation";
        private const string PriceCalculator = "PriceCalculator";

        public void Up(IDatabase db)
        {
            db.CreateTable(PriceCalculation)
              .WithId()
              .WithForeignKeyColumnTo(PriceCalculator);
        }

        public void Down(IDatabase db)
        {
            db.Tables[PriceCalculation].Drop();
        }
    }
}
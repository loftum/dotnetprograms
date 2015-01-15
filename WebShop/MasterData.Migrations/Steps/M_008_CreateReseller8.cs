using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table Reseller")]
    public class M_008_CreateReseller8 : IReversibleMigration
    {
        private const string Reseller = "Reseller";

        public void Up(IDatabase db)
        {
            db.CreateTable(Reseller)
              .WithId()
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(512);
        }

        public void Down(IDatabase db)
        {
            db.Tables[Reseller].Drop();
        }
    }
}
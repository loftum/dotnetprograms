using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table Salespoint")]
    public class M_009_CreateSalespoint9 : IReversibleMigration
    {
        private const string Salespoint = "Salespoint";
        private const string Reseller = "Reseller";

        public void Up(IDatabase db)
        {
            db.CreateTable(Salespoint)
              .WithId()
              .WithForeignKeyColumnTo(Reseller)
              .WithNotNullableColumn("Identifier", DbType.AnsiString).OfSize(50).Unique()
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(500);
            db.Tables[Salespoint].AddDefaultForeignKeyTo(Reseller);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[Salespoint];
            table.ForeignKeyTo(Reseller).Drop();
            table.Drop();
        }
    }
}
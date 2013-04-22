using System.Data;
using BasicManifest.Migrations.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Migrations.Steps
{
    [MigrationExport(Tag = "Add table Load")]
    public class M_005_AddTable_Load5 : IReversibleMigration
    {
        private const string Load = "Load";

        public void Up(IDatabase db)
        {
            db.CreateTable(Load)
              .WithId()
              .WithForeignKeyColumnTo(Tables.Camp)
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(50)
              .WithAmountColumn("DefaultSlotPrice")
              .WithChangeStamp();

            db.Tables[Load].AddDefaultForeignKeyTo(Tables.Camp);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[Load];
            table.ForeignKeyTo(Tables.Camp).Drop();
            table.Drop();
        }
    }
}
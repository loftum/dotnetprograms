using BasicManifest.Migrations.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Migrations.Steps
{
    [MigrationExport(Tag = "Add table LoadGroup")]
    public class M_006_AddTable_LoadGroup6 : IReversibleMigration
    {
        private const string LoadGroup = "LoadGroup";
        private const string Load = "Load";

        public void Up(IDatabase db)
        {
            db.CreateTable(LoadGroup)
              .WithId()
              .WithAmountColumn("Price")
              .WithForeignKeyColumnTo(Load)
              .WithChangeStamp();

            db.Tables[LoadGroup].AddDefaultForeignKeyTo(Load);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[LoadGroup];
            table.ForeignKeyTo(Load).Drop();
            table.Drop();
        }
    }
}
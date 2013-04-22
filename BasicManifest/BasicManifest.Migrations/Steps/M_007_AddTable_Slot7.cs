using BasicManifest.Migrations.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Migrations.Steps
{
    [MigrationExport(Tag = "Add table Slot")]
    public class M_007_AddTable_Slot7 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.CreateTable(Tables.Slot)
              .WithId()
              .WithAmountColumn("Price")
              .WithForeignKeyColumnTo(Tables.LoadGroup)

              .WithChangeStamp();

            db.Tables[Tables.Slot].AddDefaultForeignKeyTo(Tables.LoadGroup);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[Tables.Slot];
            table.ForeignKeyTo(Tables.LoadGroup).Drop();
            table.Drop();
        }
    }
}
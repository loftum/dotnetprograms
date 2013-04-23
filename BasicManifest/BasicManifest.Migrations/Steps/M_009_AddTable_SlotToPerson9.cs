using BasicManifest.Migrations.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Migrations.Steps
{
    [MigrationExport(Tag = "Add table SlotToPerson")]
    public class M_009_AddTable_SlotToPerson9 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.CreateCouplingTable(Tables.Slot, Tables.Skydiver);
        }

        public void Down(IDatabase db)
        {
            db.DropCouplingTable(Tables.Slot, Tables.Skydiver);
        }
    }
}
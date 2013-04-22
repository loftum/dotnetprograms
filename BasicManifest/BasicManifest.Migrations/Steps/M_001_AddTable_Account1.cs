using BasicManifest.Migrations.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Migrations.Steps
{
    [MigrationExport(Tag = "Add table Account")]
    public class M_001_AddTable_Account1 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.CreateTable(Tables.Account)
              .WithId()
              .WithAmountColumn("Balance")
              .WithChangeStamp();
        }

        public void Down(IDatabase db)
        {
            db.Tables[Tables.Account].Drop();
        }
    }
}
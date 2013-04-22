using System.Data;
using BasicManifest.Migrations.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Migrations.Steps
{
    [MigrationExport(Tag = "Add table Transaction")]
    public class M_002_AddTable_Transaction2 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.CreateTable(Tables.Transaction)
              .WithId()
              .WithForeignKeyColumnTo(Tables.Account)
              .WithNotNullableColumn("Description", DbType.AnsiString).OfSize(50)
              .WithAmountColumn("Amount")
              .WithNotNullableColumn("Type", DbType.Int32)
              .WithChangeStamp();

            db.Tables[Tables.Transaction].AddDefaultForeignKeyTo(Tables.Account);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[Tables.Transaction];
            table.ForeignKeyTo(Tables.Account).Drop();
            table.Drop();
        }
    }
}
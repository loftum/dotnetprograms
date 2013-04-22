using System.Data;
using BasicManifest.Migrations.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Migrations.Steps
{
    [MigrationExport(Tag = "Add table Camp")]
    public class M_003_AddTable_Camp3 : IReversibleMigration
    {
        private const string Camp = "Camp";

        public void Up(IDatabase db)
        {
            db.CreateTable(Camp)
              .WithId()
              .WithForeignKeyColumnTo(Tables.Account)
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(100).Unique()
              .WithAmountColumn("DefaultSlotPrice")
              .WithChangeStamp();

            db.Tables[Tables.Camp].AddDefaultForeignKeyTo(Tables.Account);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[Camp];
            table.ForeignKeyTo(Tables.Account).Drop();
            table.Drop();
        }
    }
}
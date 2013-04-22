using System.Data;
using BasicManifest.Migrations.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Migrations.Steps
{
    [MigrationExport(Tag = "Add table Day")]
    public class M_004_AddTable_Day4 : IReversibleMigration
    {
        private const string Day = "Day";

        public void Up(IDatabase db)
        {
            db.CreateTable(Day)
              .WithId()
              .WithForeignKeyColumnTo(Tables.Camp)
              .WithNotNullableColumn("Date", DbType.Date)
              .WithNotNullableColumn("IsClosed", DbType.Boolean)
              .WithChangeStamp();

            db.Tables[Day].AddDefaultForeignKeyTo(Tables.Camp);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[Day];
            table.ForeignKeyTo(Tables.Camp).Drop();
            table.Drop();
        }
    }
}
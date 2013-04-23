using System.Data;
using BasicManifest.Migrations.ExtensionMethods;
using MigSharp;

namespace BasicManifest.Migrations.Steps
{
    [MigrationExport(Tag = "Add table Skydiver")]
    public class M_008_AddTable_Skydiver8 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.CreateTable(Tables.Skydiver)
              .WithId()
              .WithForeignKeyColumnTo(Tables.Camp)
              .WithForeignKeyColumnTo(Tables.Account)
              .WithNotNullableColumn("FirstName", DbType.AnsiString).OfSize(255)
              .WithNotNullableColumn("LastName", DbType.AnsiString).OfSize(255)
              .WithNotNullableColumn("BirthDate", DbType.Date)
              .WithNotNullableColumn("Role", DbType.Int32)
              .WithChangeStamp();
            db.Tables[Tables.Skydiver].AddDefaultForeignKeyTo(Tables.Account);
        }

        public void Down(IDatabase db)
        {
            var table = db.Tables[Tables.Skydiver];
            table.ForeignKeyTo(Tables.Account).Drop();
            table.Drop();
        }
    }
}
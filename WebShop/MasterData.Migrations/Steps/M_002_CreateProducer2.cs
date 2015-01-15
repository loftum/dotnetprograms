using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table Producer")]
    public class M_002_CreateProducer2 : IReversibleMigration
    {
        private const string Producer = "Producer";

        public void Up(IDatabase db)
        {
            db.CreateTable(Producer)
              .WithId()
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(500).Unique();
        }

        public void Down(IDatabase db)
        {
            db.Tables[Producer].Drop();
        }
    }
}
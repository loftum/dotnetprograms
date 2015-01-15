using System.Data;
using MigSharp;

namespace WebShop.Migrations.Steps
{
    [MigrationExport(Tag = "Create table OrderNumber")]
    public class M_003_CreateSequenceCounter3 : IReversibleMigration
    {
        private const string SequenceCounter = "SequenceCounter";

        public void Up(IDatabase db)
        {
            db.CreateTable(SequenceCounter)
                .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(100).Unique()
                .WithNotNullableColumn("CurrentValue", DbType.Int64);

            db.Execute("insert into SequenceCounter(Name, CurrentValue) values('OrderNumber', 0)");
        }

        public void Down(IDatabase db)
        {
            db.Tables[SequenceCounter].Drop();
        }
    }
}
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Initial")]
    public class M_000_Initial0 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            
        }

        public void Down(IDatabase db)
        {
        }
    }
}
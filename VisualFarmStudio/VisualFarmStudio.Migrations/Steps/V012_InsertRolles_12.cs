using MigSharp;

namespace VisualFarmStudio.Migrations.Steps
{
    [MigrationExport]
    public class V012_InsertRolles_12 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.Execute(InsertRolle("hacker","1337 h4xx0r"));
            db.Execute(InsertRolle("bruker", "bruker"));
        }

        private static string InsertRolle(string kode, string navn)
        {
            return string.Format("insert into Rolle(Kode,navn) values ('{0}', '{1}')", kode, navn);
        }

        public void Down(IDatabase db)
        {
            db.Execute("delete from bonderolle");
            db.Execute("delete from Rolle");
        }
    }
}
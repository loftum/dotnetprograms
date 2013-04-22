using MigSharp;

namespace BasicManifest.Migrations.ExtensionMethods
{
    public static class DatabaseExtensions
    {
        public static void CreateCouplingTable(this IDatabase db, string fromTable, string toTable)
        {
            var tableName = CouplingTableName(fromTable, toTable);
            db.CreateTable(tableName)
              .WithForeignKeyColumnTo(fromTable)
              .WithForeignKeyColumnTo(toTable);

            db.Tables[tableName].AddDefaultForeignKeyTo(fromTable);
            db.Tables[tableName].AddDefaultForeignKeyTo(toTable);
        }

        public static void DropCouplingTable(this IDatabase db, string fromTable, string toTable)
        {
            var tableName = CouplingTableName(fromTable, toTable);
            var table = db.Tables[tableName];
            table.ForeignKeyTo(fromTable).Drop();
            table.ForeignKeyTo(toTable).Drop();
            table.Drop();
        }

        private static string CouplingTableName(string fromTable, string toTable)
        {
            return string.Format("{0}To{1}", fromTable, toTable);
        }
    }
}
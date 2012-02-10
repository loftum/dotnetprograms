using System.Data;
using Migrator.Framework;
using WordBank.Migrations.ExtensionMethods;

namespace WordBank.Migrations
{
    [Migration(003)]
    public class M003_Add_OrdParadigme : Migration
    {
        private const string OrdParadigmeTable = "OrdParadigme";
        private const string OrdTable = "Ord";
        private const string ParadigmeTable = "Paradigme";

        public override void Up()
        {
            Database.AddTable(OrdParadigmeTable,
                new Column("Id", DbType.Int64, ColumnProperty.PrimaryKeyWithIdentity),
                new Column(OrdTable.ForeignId(), DbType.Int64, ColumnProperty.ForeignKey),
                new Column(ParadigmeTable.ForeignId(), DbType.Int64, ColumnProperty.ForeignKey)
                );
            Database.AddForeignKey(OrdParadigmeTable, OrdTable);
            Database.AddForeignKey(OrdParadigmeTable, ParadigmeTable);
        }

        public override void Down()
        {
            Database.EmptyTable(OrdParadigmeTable);
            Database.RemoveForeignKey(OrdParadigmeTable, OrdParadigmeTable.ForeignKeyNameTo(OrdTable));
            Database.RemoveForeignKey(OrdParadigmeTable, OrdParadigmeTable.ForeignKeyNameTo(ParadigmeTable));
            Database.RemoveTable(OrdParadigmeTable);
        }
    }
}
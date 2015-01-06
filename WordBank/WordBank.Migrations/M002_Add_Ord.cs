using System.Data;
using Migrator.Framework;
using WordBank.Migrations.ExtensionMethods;

namespace WordBank.Migrations
{
    [Migration(002)]
    public class M002_Add_Ord : Migration
    {
        private const string OrdTable = "Ord";
        public override void Up()
        {
            Database.AddTable(OrdTable,
                new Column("Id", DbType.Int64, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("IdentifikasjonsNummer", DbType.Int64, ColumnProperty.NotNull),
                new Column("Grunnform", DbType.AnsiString, 1024, ColumnProperty.NotNull),
                new Column("Fullform", DbType.AnsiString, 1024, ColumnProperty.NotNull),
                new Column("MorfologiskBeskrivelse", DbType.AnsiString, 1024, ColumnProperty.Null),
                new Column("ParadigmeKode", DbType.AnsiString, 512, ColumnProperty.NotNull),
                new Column("Nummer", DbType.Int32, ColumnProperty.NotNull)
                );
        }

        public override void Down()
        {
            Database.EmptyTable(OrdTable);
            Database.RemoveTable(OrdTable);
        }
    }
}
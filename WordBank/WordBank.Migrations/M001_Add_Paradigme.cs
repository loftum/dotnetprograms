using System.Data;
using Migrator.Framework;
using WordBank.Migrations.ExtensionMethods;

namespace WordBank.Migrations
{
    [Migration(001)]
    public class M001_Add_Paradigme : Migration
    {
        private const string ParadigmeTable = "Paradigme";
        public override void Up()
        {
            Database.AddTable(ParadigmeTable, 
                new Column("Id", DbType.Int64, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Kode", DbType.AnsiString, 512, ColumnProperty.NotNull),
                new Column("Ordklasse", DbType.AnsiString, 1024, ColumnProperty.NotNull),
                new Column("Beskrivelse", DbType.AnsiString, 1024, ColumnProperty.Null),
                new Column("Fullstendig", DbType.AnsiString, 512, ColumnProperty.NotNull),
                new Column("Eksempel", DbType.AnsiString, 1024, ColumnProperty.Null),
                new Column("Nummer", DbType.Int32, ColumnProperty.NotNull),
                new Column("MorfologiskBeskrivelse", DbType.AnsiString, 1024),
                new Column("Endelser", DbType.AnsiString, 512, ColumnProperty.Null));
        }

        public override void Down()
        {
            Database.EmptyTable(ParadigmeTable);
            Database.RemoveTable(ParadigmeTable);
        }
    }
}
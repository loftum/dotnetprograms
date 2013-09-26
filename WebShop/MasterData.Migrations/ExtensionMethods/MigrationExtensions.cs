using System.Data;
using MigSharp;

namespace MasterData.Migrations.ExtensionMethods
{
    public static class MigrationExtensions
    {
        public static ICreatedTableWithAddedColumn WithId(this ICreatedTable table)
        {
            return table.WithPrimaryKeyColumn("Id", DbType.Guid);
        }

        public static ICreatedTableWithAddedColumn WithForeignKeyColumnTo(this ICreatedTableBase table, string otherTable, bool required = true)
        {
            return required
                ? table.WithNotNullableColumn(ForeignKeyColumnTo(otherTable), DbType.Guid)
                : table.WithNullableColumn(ForeignKeyColumnTo(otherTable), DbType.Guid);
        }

        public static ICreatedTableWithAddedColumn WithForeignKeyColumnTo(this ICreatedTableBase table, string otherTable, string columnName, bool required = true)
        {
            return required
                ? table.WithNotNullableColumn(ForeignKeyColumnTo(otherTable), DbType.Guid)
                : table.WithNullableColumn(ForeignKeyColumnTo(otherTable), DbType.Guid);
        }

        public static IAddedForeignKey AddDefaultForeignKeyTo(this IExistingTable table, string otherTable)
        {
            return table.AddForeignKeyTo(otherTable).Through(ForeignKeyColumnTo(otherTable), "Id");
        }

        private static string ForeignKeyColumnTo(string table)
        {
            return string.Format("{0}_Id", table);
        } 
    }
}
using System.Data;
using MigSharp;

namespace BasicManifest.Migrations.ExtensionMethods
{
    public static class CreatedTableExtensions
    {
        public static ICreatedTableWithAddedColumn WithId(this ICreatedTable table)
        {
            return table.WithPrimaryKeyColumn("Id", DbType.Guid);
        }

        public static ICreatedTableWithAddedColumn WithChangeStamp(this ICreatedTableWithAddedColumn table)
        {
            return table.WithNotNullableColumn("CreatedDate", DbType.DateTime)
                        .WithNotNullableColumn("CreatedBy", DbType.AnsiString).OfSize(50)
                        .WithNotNullableColumn("ModifiedDate", DbType.DateTime)
                        .WithNotNullableColumn("ModifiedBy", DbType.AnsiString).OfSize(50);
        }

        public static ICreatedTableWithAddedColumn WithForeignKeyColumnTo<TCreatedTable>(this TCreatedTable table, string foreignTable, bool mandatory = true)
            where TCreatedTable : ICreatedTableBase
        {
            var column = ForeginKeyColumnTo(foreignTable);
            return mandatory
                ? table.WithNotNullableColumn(column, DbType.Guid)
                : table.WithNullableColumn(column, DbType.Guid);
        }

        public static ICreatedTableWithAddedColumn WithAmountColumn<TCreatedTable>(this TCreatedTable table, string columnName, bool mandatory = true)
            where TCreatedTable : ICreatedTableBase
        {
            return mandatory
                ? table.WithNotNullableColumn(columnName, DbType.Decimal).OfSize(8, 2)
                : table.WithNullableColumn(columnName, DbType.Decimal).OfSize(8, 2);
        }

        private static string ForeginKeyColumnTo(string foreignTable)
        {
            return string.Format("{0}Id", foreignTable);
        }

        public static IAddedForeignKey AddDefaultForeignKeyTo(this IExistingTable table, string foreignTable)
        {
            return table.AddForeignKeyTo(foreignTable).Through(ForeginKeyColumnTo(foreignTable), "Id");
        }
    }
}
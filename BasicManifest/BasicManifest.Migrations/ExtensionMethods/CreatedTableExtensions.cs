using System.Data;
using MigSharp;

namespace BasicManifest.Migrations.ExtensionMethods
{
    public static class CreatedTableExtensions
    {
        private const DbType IdType = DbType.Int64;

        public static ICreatedTableWithAddedColumn WithId(this ICreatedTable table)
        {
            return table.WithPrimaryKeyColumn("Id", IdType).AsIdentity();
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
                ? table.WithNotNullableColumn(column, IdType)
                : table.WithNullableColumn(column, IdType);
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
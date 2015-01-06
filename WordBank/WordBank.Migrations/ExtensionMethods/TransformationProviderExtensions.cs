using Migrator.Framework;

namespace WordBank.Migrations.ExtensionMethods
{
    public static class TransformationProviderExtensions
    {
        public static void AddForeignKey(this ITransformationProvider provider, string foreignTable, string primaryTable,
            ForeignKeyConstraint constraint = ForeignKeyConstraint.NoAction)
        {
            provider.AddForeignKey(foreignTable.ForeignKeyNameTo(primaryTable),
                foreignTable,
                primaryTable.ForeignId(),
                primaryTable,
                "Id",
                constraint);
        }

        public static string ForeignKeyNameTo(this string foreignTable, string primaryTable)
        {
            return string.Format("FK_{0}_{1}", foreignTable, primaryTable);
        }
        
        public static string ForeignId(this string tableName)
        {
            return string.Format("{0}Id", tableName);
        }

        public static void EmptyTable(this ITransformationProvider provider, string tableName)
        {
            provider.ExecuteNonQuery(string.Format("delete from {0}", tableName));
        }
    }
}
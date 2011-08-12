using System.Collections.Generic;
using FluentMigrator.Builders.Create.Table;

namespace StuffLibrary.Migrations.ExtensionMethods
{
    public static class MigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("Id")
                .AsInt64()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithVersionColumn(this ICreateTableWithColumnSyntax tableColumnAsTypeSyntax)
        {
            return tableColumnAsTypeSyntax
                .WithColumn("Version")
                .AsInt64()
                .NotNullable()
                .WithDefaultValue(0);
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithTimeStamps(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("CreatedBy").AsString().Nullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("ModifiedBy").AsString().Nullable()
                .WithColumn("ModifiedAt").AsDateTime().NotNullable();
        }

        private static string ForeignKeyName(string foreignTable)
        {
            return string.Format("FK_{0}", foreignTable);
        }

        private static string ForeignColumnName(string foreignTable)
        {
            return string.Format("{0}Id", foreignTable);
        }
    }
}
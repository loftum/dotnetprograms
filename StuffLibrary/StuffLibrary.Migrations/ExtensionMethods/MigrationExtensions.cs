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
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("ModifiedAt").AsDateTime().NotNullable();
        }
    }
}
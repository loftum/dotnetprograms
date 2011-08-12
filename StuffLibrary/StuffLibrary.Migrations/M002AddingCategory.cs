using FluentMigrator;
using StuffLibrary.Migrations.ExtensionMethods;

namespace StuffLibrary.Migrations
{
    [Migration(002)]
    public class M002AddingCategory : Migration
    {
        public override void Up()
        {
            Create.Table("Category")
                .WithIdColumn()
                .WithVersionColumn()
                .WithTimeStamps()
                .WithColumn("Name").AsString().NotNullable().Unique();
        }

        public override void Down()
        {
            Delete.Table("Category");
        }
    }
}
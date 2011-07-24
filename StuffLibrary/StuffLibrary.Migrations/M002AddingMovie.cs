using FluentMigrator;
using StuffLibrary.Migrations.ExtensionMethods;

namespace StuffLibrary.Migrations
{
    [Migration(002)]
    public class M002AddingMovie : Migration
    {
        public override void Up()
        {
            Create.Table("Movie")
                .WithIdColumn()
                .WithVersionColumn()
                .WithColumn("Title").AsString().NotNullable()
                .WithTimeStamps();
        }

        public override void Down()
        {
            Delete.Table("Movie");
        }
    }
}
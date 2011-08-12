using FluentMigrator;
using StuffLibrary.Migrations.ExtensionMethods;

namespace StuffLibrary.Migrations
{
    [Migration(003)]
    public class M003AddingMovie : Migration
    {
        public override void Up()
        {
            Create.Table("Movie")
                .WithIdColumn()
                .WithVersionColumn()
                .WithTimeStamps()
                .WithColumn("Title").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Movie");
        }
    }
}
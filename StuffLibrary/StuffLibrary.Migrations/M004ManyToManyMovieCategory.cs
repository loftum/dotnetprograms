using FluentMigrator;

namespace StuffLibrary.Migrations
{
    [Migration(004)]
    public class M004ManyToManyMovieCategory : Migration
    {
        public override void Up()
        {
            Create.Table("MovieCategory")
                .WithColumn("MovieId").AsInt64().NotNullable()
                .WithColumn("CategoryId").AsInt64().NotNullable();
            
            Create.ForeignKey()
                .FromTable("MovieCategory").ForeignColumn("MovieId")
                .ToTable("Movie").PrimaryColumn("Id");

            Create.ForeignKey()
                .FromTable("MovieCategory").ForeignColumn("CategoryId")
                .ToTable("Category").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("MovieCategory");
        }
    }
}
using System.Data;
using Migrator.Framework;

namespace MovieBase.Migrations.Versions
{
    [Migration(000001)]
    public class V000001AddMovieAndCategory : Migration
    {
        private const string MovieTableName = "Movie";
        private const string CategoryTableName = "Category";
        private const string MovieCategoryTableName = "MovieCategory";

        public override void Up()
        {
            Database.AddTable(CategoryTableName,
                new Column("Id", DbType.Int64, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Name", DbType.String, ColumnProperty.NotNull));
            Database.AddTable(MovieTableName,
                new Column("Id", DbType.Int64, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Title", DbType.String, ColumnProperty.NotNull));
            Database.AddTable(MovieCategoryTableName,
                new Column("Id", DbType.Int64, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("MovieId", DbType.Int64, ColumnProperty.ForeignKey),
                new Column("CategoryId", DbType.Int64, ColumnProperty.ForeignKey));
        }

        public override void Down()
        {
            Database.RemoveTable(MovieCategoryTableName);
            Database.RemoveTable(MovieTableName);
            Database.RemoveTable(CategoryTableName);
        }
    }
}
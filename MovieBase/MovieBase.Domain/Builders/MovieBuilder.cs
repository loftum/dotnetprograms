namespace MovieBase.Domain.Builders
{
    public class MovieBuilder : BuilderBase<Movie>
    {
        private MovieBuilder(Movie movie) : base(movie)
        {
        }

        public static MovieBuilder Movie()
        {
            var movie = new Movie();
            return new MovieBuilder(movie);
        }

        public MovieBuilder WithTitle(string title)
        {
            Item.Title = title;
            return this;
        }

        public MovieBuilder WithCategory(CategoryBuilder builder)
        {
            return WithCategory(builder.Item);
        }

        public MovieBuilder WithCategory(Category category)
        {
            Item.AddCategory(category);
            return this;
        }

        public MovieBuilder WithCategories(params Category[] categories)
        {
            foreach (var category in categories)
            {
                WithCategory(category);
            }
            return this;
        }
    }
}
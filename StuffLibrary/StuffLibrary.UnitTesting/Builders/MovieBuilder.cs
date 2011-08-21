using StuffLibrary.Domain;

namespace StuffLibrary.UnitTesting.Builders
{
    public class MovieBuilder : DomainBuilderBase<MovieBuilder, Movie>
    {
        protected MovieBuilder(Movie item) : base(item)
        {
        }

        public static MovieBuilder New()
        {
            return new MovieBuilder(NewItem());
        }

        public static MovieBuilder Existing()
        {
            return new MovieBuilder(ExistingItem());
        }

        public MovieBuilder WithTitle(string title)
        {
            Item.Title = title;
            return MySelf;
        }
    }
}
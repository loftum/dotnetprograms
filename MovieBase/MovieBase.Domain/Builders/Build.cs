namespace MovieBase.Domain.Builders
{
    public class Build
    {
        public static MovieBuilder Movie()
        {
            return MovieBuilder.Movie();
        }

        public static CategoryBuilder Category()
        {
            return CategoryBuilder.Category();
        }
    }
}
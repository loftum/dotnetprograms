namespace StuffLibrary.UnitTesting.Builders
{
    public class Build
    {
        public static MovieBuilder Movie()
        {
            return MovieBuilder.Existing();
        }

        public static MovieBuilder NewMovie()
        {
            return MovieBuilder.New();
        }
    }
}
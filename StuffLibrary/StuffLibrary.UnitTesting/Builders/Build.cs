namespace StuffLibrary.UnitTesting.Builders
{
    public class Build
    {
        public static MovieBuilder Movie()
        {
            return MovieBuilder.Existing();
        }
    }
}
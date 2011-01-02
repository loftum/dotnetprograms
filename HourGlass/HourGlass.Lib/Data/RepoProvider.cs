namespace HourGlass.Lib.Data
{
    public class RepoProvider
    {
        public static IHourGlassRepo GetRepo()
        {
            return new HourGlassRepo(SessionFactoryProvider.SqliteSessionFactory("HourGlass.db"));
        }
    }
}
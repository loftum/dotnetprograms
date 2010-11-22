namespace MovieBase.Migrations
{
    public class MovieBaseMigrator
    {
        private readonly Migrator.Migrator _migrator;

        public MovieBaseMigrator(string connectionString)
        {
            _migrator = new Migrator.Migrator("mysql", connectionString, GetType().Assembly);
        }

        public MovieBaseMigrator MigrateTo(long version)
        {
            _migrator.MigrateTo(version);
            return this;
        }

        public MovieBaseMigrator MigrateToLatest()
        {
            _migrator.MigrateToLastVersion();
            return this;
        }
    }
}
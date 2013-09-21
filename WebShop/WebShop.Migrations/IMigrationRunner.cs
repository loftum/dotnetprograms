namespace WebShop.Migrations
{
    public interface IMigrationRunner
    {
        long MigrateUp();
        long MigrateDown();
        long MigrateTo(long version);
    }
}
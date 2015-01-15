namespace WebShop.Migrations
{
    public interface IWebShopMigrator
    {
        long MigrateUp();
        long MigrateDown();
        long MigrateTo(long version);
    }
}
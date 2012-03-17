namespace VisualFarmStudio.Lib.Migrating
{
    public interface IVisualFarmMigrator
    {
        void MigrateUp();
        void MigrateDown();
        string GetVersion();
    }
}
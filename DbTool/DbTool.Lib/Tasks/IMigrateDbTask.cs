namespace DbTool.Lib.Tasks
{
    public interface IMigrateDbTask
    {
        void MigrateTo(long version);
        void MigrateToLatest();
    }
}
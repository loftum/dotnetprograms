namespace DbTool.Lib.Tasks
{
    public interface IBackupTask : IProgressTask
    {
        void Backup(BackupParameters parameters);
    }
}
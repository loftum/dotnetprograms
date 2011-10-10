namespace DbTool.Lib.Tasks
{
    public interface IBackupTask
    {
        event TaskProgressEventHandler PercentComplete;
        event TaskProgressCompleteEventHandler Complete;
        void Backup(BackupParameters parameters);
    }
}
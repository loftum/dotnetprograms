namespace DbTool.Lib.Tasks
{
    public delegate void TaskProgressCompleteEventHandler(object sender, TaskProgressCompleteEventArgs args);
    public delegate void TaskProgressEventHandler(object sender, TaskProgressEventArgs args);

    public interface IRestoreTask
    {
        event TaskProgressEventHandler PercentComplete;
        event TaskProgressCompleteEventHandler Complete;
        void Restore(BackupParameters parameters);
    }
}
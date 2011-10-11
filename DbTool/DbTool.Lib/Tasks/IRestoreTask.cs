namespace DbTool.Lib.Tasks
{
    public interface IRestoreTask : IProgressTask
    {
        void Restore(BackupParameters parameters);
    }
}
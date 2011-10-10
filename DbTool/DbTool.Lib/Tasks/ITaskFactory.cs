using DbTool.Lib.Configuration;

namespace DbTool.Lib.Tasks
{
    public interface ITaskFactory
    {
        IBackupTask CreateBackupTask(ConnectionData connection);
        IRestoreTask CreateRestoreTask(ConnectionData connection);
    }
}
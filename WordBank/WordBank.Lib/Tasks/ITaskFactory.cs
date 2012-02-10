namespace WordBank.Lib.Tasks
{
    public interface ITaskFactory
    {
        IImportTask CreateImportTask();
    }
}
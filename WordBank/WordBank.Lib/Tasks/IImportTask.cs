namespace WordBank.Lib.Tasks
{
    public interface IImportTask
    {
        void Import(string paradigmeFile, string fullFormFile);
    }
}
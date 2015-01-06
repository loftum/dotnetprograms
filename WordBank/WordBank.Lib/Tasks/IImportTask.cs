namespace WordBank.Lib.Tasks
{
    public interface IImportTask
    {
        void Import(string paradigmeFile, string fullFormFile);
        void ImportParadigme(string paradigmeFile);
        void ImportFullForm(string fullFormFile);
    }
}
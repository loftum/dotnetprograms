namespace MongoTool.Core.WorkSheets
{
    public interface IWorkSheetManager
    {
        string Load();
        void Save(string text);
    }
}
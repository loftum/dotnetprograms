namespace DbTool.Lib.Ui.Worksheet
{
    public interface IWorksheetManager
    {
        void Save(string worksheetText);
        string Read();
    }
}
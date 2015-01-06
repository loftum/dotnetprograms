using System.IO;

namespace MongoTool.Core.WorkSheets
{
    public class WorkSheetManager : IWorkSheetManager
    {
        private readonly string _fileName;

        public WorkSheetManager()
        {
            _fileName = "worksheet.cs";
        }

        public string Load()
        {
            if (File.Exists(_fileName))
            {
                return File.ReadAllText(_fileName);
            }
            return null;
        }

        public void Save(string text)
        {
            File.WriteAllText(_fileName, text);
        }
    }
}
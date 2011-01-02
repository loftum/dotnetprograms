using System;
using System.Text;
using Deploy.Lib.Values;

namespace Deploy.Lib.FilenameGenerating
{
    public class FilenameGenerator
    {
        public string BaseDdMmYyyyHhMmSsExtension(string baseName, string extension)
        {
            var dateValue = new DateValue(DateTime.Now);
            return new StringBuilder(baseName)
                .Append("_")
                .Append(dateValue.DdMmYyyy())
                .Append("_")
                .Append(dateValue.HhMmSs())
                .Append(InsertDot(extension))
                .ToString();
        }

        public string BaseYyyyMmDdHhMmSsExtension(string baseName, string extension)
        {
            var dateValue = new DateValue(DateTime.Now);
            return new StringBuilder(baseName)
                .Append("_")
                .Append(dateValue.YyyyMmDd())
                .Append("_")
                .Append(dateValue.HhMmSs())
                .Append(InsertDot(extension))
                .ToString();
        }

        private static string InsertDot(string extension)
        {
            return new StringBuilder(extension.Replace(".", string.Empty))
                .Insert(0, ".")
                .ToString();
        }
    }
}

using System.IO;

namespace Wordbank.Lib.ExtensionMethods
{
    public static class FileExtensions
    {
        public static bool Exists(this string filePath)
        {
            filePath.ShouldNotBeNull("filePath");
            return File.Exists(filePath);
        }

        public static string ToFullPath(this string file)
        {
            file.ShouldNotBeNull("file");
            var path = Path.IsPathRooted(file) ? file : Path.Combine(".", file);
            return Path.GetFullPath(path);
        }
    }
}
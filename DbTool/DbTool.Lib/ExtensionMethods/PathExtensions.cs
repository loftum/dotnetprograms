using System.IO;

namespace DbTool.Lib.ExtensionMethods
{
    public static class PathExtensions
    {
        public static bool IsFullPath(this string path)
        {
            path.ShouldNotBeNullOrWhitespace("path");
            return Path.IsPathRooted(path) && File.Exists(path);
        }

        public static bool Exists(this string path)
        {
            path.ShouldNotBeNullOrWhitespace("path");
            return File.Exists(path);
        }

        public static string CombineWith(this string rootPath, params string[] paths)
        {
            rootPath.ShouldNotBeNullOrWhitespace("rootPath");
            var allPaths = rootPath.ToListWith(paths);
            return Path.Combine(allPaths.ToArray());
        }
    }
}
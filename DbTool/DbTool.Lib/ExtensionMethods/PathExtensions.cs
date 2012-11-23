using System.IO;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Validation;

namespace DbTool.Lib.ExtensionMethods
{
    public static class PathExtensions
    {
        public static bool IsFullPath(this string path)
        {
            Guard.NotNullOrWhiteSpace(() => path);
            return Path.IsPathRooted(path) && File.Exists(path);
        }

        public static bool Exists(this string path)
        {
            Guard.NotNullOrWhiteSpace(() => path);
            return File.Exists(path);
        }

        public static string CombineWith(this string rootPath, params string[] paths)
        {
            Guard.NotNullOrWhiteSpace(() => rootPath);
            var allPaths = rootPath.ToListWith(paths);
            return Path.Combine(allPaths.ToArray());
        }
    }
}
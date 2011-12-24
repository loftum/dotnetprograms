using System.IO;
using System.Reflection;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.FileSystem
{
    public class PathResolver : IPathResolver
    {
        public string GetFullPathOfExisting(string filename)
        {
            var fullPath = GetFullPathOf(filename);
            if (!fullPath.Exists())
            {
                throw new DbToolException("Could not find file {0}", fullPath);
            }
            return fullPath;
        }

        public string GetFullPathOf(string filename)
        {
            filename.ShouldNotBeNullOrWhitespace("filename");

            if (filename.IsFullPath())
            {
                return filename;
            }

            var currentDir = GetCurrentDir();
            return currentDir.CombineWith(filename);
        }

        private static string GetCurrentDir()
        {
            var executingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(executingAssemblyLocation);
        }
    }
}
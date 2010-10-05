using System.Collections.Generic;
using System.IO;

namespace Wildcard.Replacement
{
    public class ReplacementParameters
    {
        public string WildcardFilePath { get; private set; }
        public string FilePath { get; private set; }

        private ReplacementParameters(string wildcardFilePath, string filePath)
        {
            WildcardFilePath = wildcardFilePath;
            FilePath = filePath;
        }

        public static ReplacementParameters Parse(string[] args)
        {
            ValidateFilePaths(args);
            return new ReplacementParameters(args[0], args[1]);
        }

        private static void ValidateFilePaths(IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                VerifyExists(path);
            }
        }

        private static void VerifyExists(string path)
        {
            if (!File.Exists(path))
            {
                throw new ReplacementException(path + " does not exist.");
            }
        }
    }
}

using System.Collections.Generic;
using System.IO;
using Deploy.Lib.FileManagement;
using Deploy.Lib.RegularExpressions;

namespace DeployWizard.Lib.AutoComplete.FileSystem
{
    public class FileSystemAutoCompleteProvider : IAutoCompleteProvider
    {
        private readonly IFileSystemManager _fileSystemManager;
        private readonly CompletionType _completionType;

        public FileSystemAutoCompleteProvider(IFileSystemManager fileSystemManager)
            : this(fileSystemManager, CompletionType.FilesAndFolders)
        {
        }

        public FileSystemAutoCompleteProvider(IFileSystemManager fileSystemManager, CompletionType completionType)
        {
            _fileSystemManager = fileSystemManager;
            _completionType = completionType;
        }

        public IEnumerable<string> GetSuggestionsFor(string input)
        {
            return GetSuggestionsFor(input, string.Empty);
        }

        private IEnumerable<string> GetSuggestionsFor(string input, string searchPattern)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new string[0];
            }
            if (!_fileSystemManager.DirectoryExists(input))
            {
                var path = NearestPathOf(input);
                var head = HeadOf(input);
                return GetSuggestionsFor(path, Regexifier.Regexify(head).WithTrailingWildcard().Pattern);
            }

            switch (_completionType)
            {
                case CompletionType.FilesOnly:
                    return _fileSystemManager.FilenamesIn(input);
                case CompletionType.FoldersOnly:
                    return _fileSystemManager.FoldernamesIn(input);
                default:
                    return _fileSystemManager.FileAndFolderNamesIn(input);
            }
        }

        private static string HeadOf(string input)
        {
            return !input.Contains(Path.DirectorySeparatorChar.ToString()) || input.EndsWith(Path.DirectorySeparatorChar.ToString()) ? 
                string.Empty : 
                input.Substring(input.LastIndexOf(Path.PathSeparator) + 1);
        }

        private static string NearestPathOf(string input)
        {
            return input.Substring(0, input.LastIndexOf(Path.DirectorySeparatorChar));
        }
    }
}
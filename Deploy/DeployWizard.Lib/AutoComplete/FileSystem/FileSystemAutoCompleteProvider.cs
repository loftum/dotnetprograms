using System;
using System.Collections.Generic;
using System.IO;
using Deploy.Lib.FileManagement;

namespace DeployWizard.Lib.AutoComplete.FileSystem
{
    public class FileSystemAutoCompleteProvider : IAutoCompleteProvider
    {
        private readonly IFileSystemManager _fileSystemManager;
        private readonly CompletionType _completionType;

        public FileSystemAutoCompleteProvider(IFileSystemManager fileSystemManager) : 
            this(fileSystemManager, CompletionType.FilesAndFolders)
        {
        }

        public FileSystemAutoCompleteProvider(IFileSystemManager fileSystemManager, CompletionType completionType)
        {
            _fileSystemManager = fileSystemManager;
            _completionType = completionType;
        }

        public IEnumerable<string> GetSuggestionsFor(string input)
        {
            var suggestions = new List<string>();
            if (string.IsNullOrWhiteSpace(input))
            {
                return suggestions;
            }
            var directory = GetDirectory(input);
            if (string.IsNullOrEmpty(directory))
            {
                return suggestions;
            }
            var startOfNextEntry = GetStartOfNextEntry(input);

            switch(_completionType)
            {
                case CompletionType.FilesOnly:
                    return _fileSystemManager.FilenamesIn(directory, startOfNextEntry);
                case CompletionType.FoldersOnly:
                    return _fileSystemManager.FoldernamesIn(directory, startOfNextEntry);
                default:
                    return _fileSystemManager.FileAndFolderNamesIn(directory, startOfNextEntry);
            }
        }

        private string GetDirectory(string input)
        {
            if (_fileSystemManager.DirectoryExists(input))
            {
                return input;
            }
            return GetNearestDirectory(input);
        }

        private string GetStartOfNextEntry(string input)
        {
            if (_fileSystemManager.DirectoryExists(input))
            {
                return string.Empty;
            }
            return input.Substring(input.LastIndexOf(Path.DirectorySeparatorChar) + 1) + "*";
        }

        private string GetNearestDirectory(string input)
        {
            if (input.Contains(Path.DirectorySeparatorChar.ToString()))
            {
                var candidate = input.Substring(0, input.LastIndexOf(Path.DirectorySeparatorChar));
                if (_fileSystemManager.DirectoryExists(candidate))
                {
                    return candidate;    
                }
            }
            return string.Empty;
        }
    }
}

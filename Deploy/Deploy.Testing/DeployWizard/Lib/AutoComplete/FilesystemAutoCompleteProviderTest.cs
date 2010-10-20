using System.Collections.Generic;
using Deploy.Lib.FileManagement;
using DeployWizard.Lib.AutoComplete.FileSystem;
using NUnit.Framework;
using Rhino.Mocks;

namespace Deploy.Testing.DeployWizard.Lib.AutoComplete
{
    [TestFixture]
    public class FilesystemAutoCompleteProviderTest
    {
        private FileSystemAutoCompleteProvider _provider;
        private IFileSystemManager _fileSystemManagerMock;
        private MockRepository _mockRepository;
        private static readonly IEnumerable<string> SomeFileAndFolders = new [] {"file1", "file2", "folder2", "folder2"};
        private static readonly IEnumerable<string> SomeFiles = new []{"file1", "file2"};
        private static readonly IEnumerable<string> SomeFolders = new[] {"folder1", "folder2"};
        private const string SomeFolder = @"C:\someFolder";

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository();
            _fileSystemManagerMock = _mockRepository.DynamicMock<IFileSystemManager>();
        }

        [Test]
        public void ShouldReturnEmptyListWhenInputIsEmpty()
        {
            _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock);
            CollectionAssert.AreEqual(_provider.GetSuggestionsFor(string.Empty), new string[0]);
        }

        [Test]
        public void ShouldReturnEmptyListWhenInputIsWhitespace()
        {
            _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock);
            CollectionAssert.AreEqual(new string[0], _provider.GetSuggestionsFor("   "));
        }

        [Test]
        public void ShouldGetFilesAndFolders()
        {
            using (_mockRepository.Record())
            {
                Expect.Call(_fileSystemManagerMock.DirectoryExists(SomeFolder)).Return(true);
                Expect.Call(_fileSystemManagerMock.FileAndFolderNamesIn(SomeFolder, string.Empty)).Repeat.Once()
                    .Return(SomeFileAndFolders);
            }
            using (_mockRepository.Playback())
            {
                _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock);
                var suggestions = _provider.GetSuggestionsFor(SomeFolder);
                CollectionAssert.AreEqual(SomeFileAndFolders, suggestions);
            }
        }

        [Test]
        public void ShouldGetFilsOnlyIfSpecified()
        {
            using (_mockRepository.Record())
            {
                Expect.Call(_fileSystemManagerMock.DirectoryExists(SomeFolder)).Return(true);
                Expect.Call(_fileSystemManagerMock.FilenamesIn(SomeFolder, string.Empty))
                    .Return(SomeFiles);
            }
            using (_mockRepository.Playback())
            {
                _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock, CompletionType.FilesOnly);
                var suggestions = _provider.GetSuggestionsFor(SomeFolder);
                CollectionAssert.AreEqual(SomeFiles, suggestions);
            }
        }

        [Test]
        public void ShouldGetFoldersOnlyIfSpecified()
        {
            using (_mockRepository.Record())
            {
                Expect.Call(_fileSystemManagerMock.DirectoryExists(SomeFolder)).Return(true);
                Expect.Call(_fileSystemManagerMock.FoldernamesIn(SomeFolder, string.Empty)).Repeat.Once()
                    .Return(SomeFolders);
            }
            using (_mockRepository.Playback())
            {
                _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock, CompletionType.FoldersOnly);
                var suggestions = _provider.GetSuggestionsFor(SomeFolder);
                CollectionAssert.AreEqual(SomeFolders, suggestions);
            }
        }

        [Test]
        public void ShouldGetFilesAndFoldersMatchingEndOfInputIfInputIsNotFileOrFolder()
        {
            using (_mockRepository.Record())
            {
                Expect.Call(_fileSystemManagerMock.DirectoryExists(SomeFolder + @"\a")).Return(false);
                Expect.Call(_fileSystemManagerMock.FileAndFolderNamesIn(SomeFolder, "a*")).Return(SomeFileAndFolders);
            }
            using (_mockRepository.Playback())
            {
                _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock);
                var suggestions = _provider.GetSuggestionsFor(SomeFolder + @"\a");
                CollectionAssert.AreEqual(SomeFileAndFolders, suggestions);
            }
        }
    }
}

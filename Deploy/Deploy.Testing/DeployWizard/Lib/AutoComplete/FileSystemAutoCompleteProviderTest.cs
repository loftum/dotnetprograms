using System.Collections.Generic;
using System.IO;
using Deploy.Lib.FileManagement;
using DeployWizard.Lib.AutoComplete.FileSystem;
using NUnit.Framework;
using Rhino.Mocks;

namespace Deploy.Testing.DeployWizard.Lib.AutoComplete
{
    [TestFixture]
    public class FileSystemAutoCompleteProviderTest
    {
        private FileSystemAutoCompleteProvider _provider;
        private IFileSystemManager _fileSystemManagerMock;
        private MockRepository _mockRepository;

        private const string SomePath = @"C:\some\path";
        private static readonly IEnumerable<string> SomeFilesAndFolders = new[] { "file1", "folder1", "file2", "folder2", "whateverThereIs" };
        private static readonly IEnumerable<string> SomeFiles = new[] {"file1", "file2"};
        private static readonly IEnumerable<string> SomeFolders = new[] {"folder1", "folder2"};

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository();
            _fileSystemManagerMock = _mockRepository.DynamicMock<IFileSystemManager>();
        }

        [Test]
        public void ShouldSuggestFilesAndFoldersInFolder()
        {
            _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock);
            using (_mockRepository.Record())
            {
                Expect.Call(_fileSystemManagerMock.DirectoryExists(SomePath)).Return(true);
                Expect.Call(_fileSystemManagerMock.FileAndFolderNamesIn(SomePath)).Return(SomeFilesAndFolders);
            }
            using (_mockRepository.Playback())
            {
                var suggestions = _provider.GetSuggestionsFor(SomePath);
                CollectionAssert.AreEqual(SomeFilesAndFolders, suggestions);
            }
        }

        [Test]
        public void ShouldSuggestFilesInFolderWhenFilesOnlyIsSpesicifed()
        {
            _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock, CompletionType.FilesOnly);
            using (_mockRepository.Record())
            {
                Expect.Call(_fileSystemManagerMock.DirectoryExists(SomePath)).Return(true);
                Expect.Call(_fileSystemManagerMock.FilenamesIn(SomePath)).Return(SomeFiles);
            }
            using (_mockRepository.Playback())
            {
                var suggestions = _provider.GetSuggestionsFor(SomePath);
                CollectionAssert.AreEqual(SomeFiles, suggestions);
            }
        }

        [Test]
        public void ShouldSuggestFoldersWhenFoldersOnlyIsSpecified()
        {
            _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock, CompletionType.FoldersOnly);
            using (_mockRepository.Record())
            {
                Expect.Call(_fileSystemManagerMock.DirectoryExists(SomePath)).Return(true);
                Expect.Call(_fileSystemManagerMock.FoldernamesIn(SomePath)).Return(SomeFolders);
            }
            using (_mockRepository.Playback())
            {
                var suggestions = _provider.GetSuggestionsFor(SomePath);
                CollectionAssert.AreEqual(SomeFolders, suggestions);
            }
        }

        [Test]
        public void ShouldStripPathAndSearchForFilesStartingWithWhatever()
        {
            var searchString = Path.Combine(SomePath, "whatever");
            _provider = new FileSystemAutoCompleteProvider(_fileSystemManagerMock);
            var result = new[] {"whateverThereIs"};
            using (_mockRepository.Record())
            {
                Expect.Call(_fileSystemManagerMock.DirectoryExists(searchString)).Return(false);
                Expect.Call(_fileSystemManagerMock.DirectoryExists(SomePath)).Return(true);
                Expect.Call(_fileSystemManagerMock.FileAndFolderNamesIn(SomePath)).Return(result);
            }
            using (_mockRepository.Playback())
            {
                var suggestions = _provider.GetSuggestionsFor(searchString);
                CollectionAssert.AreEqual(result, suggestions);
            }
        }
    }
}
using System.IO;
using Deploy.Lib.FileManagement;
using Deploy.Testing.Assertions;
using NUnit.Framework;

namespace Deploy.IntegrationTests.Deploy.Lib.FileManagement
{
    [TestFixture]
    public class FileSystemManagerTest
    {
        private readonly FileSystemManager _fileSystemManager = new FileSystemManager();

        private const string TestFolderName = "TestFolder";
        private const string TestSubFolderName = "TestSubFolder";
        private const string TestFileName = "file.txt";
        private static readonly string TestFolderPath = Path.Combine(".", TestFolderName);
        private static readonly string TestSubFolderPath = Path.Combine(TestFolderPath, TestSubFolderName);
        private static readonly string TestFilePath = Path.Combine(TestFolderPath, TestFileName);

        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(TestFolderPath);
            Directory.CreateDirectory(TestSubFolderPath);
            using (File.Create(TestFilePath))
            {
            }
        }

        [TearDown]
        public void Cleanup()
        {
            Directory.Delete(TestFolderPath, true);
        }

        [Test]
        public void DirectoryExistsShouldReturnTrueWhenDirectoryExists()
        {
            Assert.That(_fileSystemManager.DirectoryExists(TestFolderPath));
        }

        [Test]
        public void DirectoryExistsShouldReturnFalseWhenDirectoryDoesNotExist()
        {
            Assert.That(_fileSystemManager.DirectoryExists("nonexistingLalala"), Is.False);
        }

        [Test]
        public void FileExistsShouldReturnTrueWhenFileExists()
        {
            Assert.That(_fileSystemManager.FileExists(TestFilePath));
        }

        [Test]
        public void FileExistsShouldReturnFalseWhenFileDoesNotExist()
        {
            Assert.That(_fileSystemManager.FileExists("nonexistingLalala"), Is.False);
        }

        [Test]
        public void FileAndFolderNamesInShouldGetFileAndFolderNames()
        {
            var filesAndFolders = _fileSystemManager.FileAndFolderNamesIn(TestFolderPath);

            CollectionAsserter<string>.AssertThat(filesAndFolders)
                .Contains(TestSubFolderName)
                .Contains(TestFileName);
        }

        [Test]
        public void FoldernamesInShouldGetOnlyFolderNames()
        {
            var folders = _fileSystemManager.FoldernamesIn(TestFolderPath);
            CollectionAsserter<string>.AssertThat(folders).Contains(TestSubFolderName).NotContains(TestFileName);
        }

        [Test]
        public void FilenamesInShouldGetOnlyFilenames()
        {
            var files = _fileSystemManager.FilenamesIn(TestFolderPath);
            CollectionAsserter<string>.AssertThat(files).Contains(TestFileName).NotContains(TestSubFolderName);
        }

        [Test]
        public void ShouldCreateNewDirectory()
        {
            var newDirectoryPath = Path.Combine(TestFolderPath, "newFolder");
            var directory = _fileSystemManager.CreateNewDirectory(newDirectoryPath);
            Assert.That(directory.Exists);
        }

        [Test]
        public void ShouldCreateNewFile()
        {
            var newFilePath = Path.Combine(TestFolderPath, "newFile.txt");
            var file =_fileSystemManager.CreateNewFile(newFilePath);
            Assert.That(file.Exists);
        }
    }
}
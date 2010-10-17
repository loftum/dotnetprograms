using System.IO;

namespace Deploy.Lib.FileManagement
{
    public class DirectoryCopier
    {
        private readonly DirectoryInfo _directory;

        public DirectoryCopier(DirectoryInfo directory)
        {
            _directory = directory;
        }

        public static DirectoryCopier CopyContentsOf(DirectoryInfo directory)
        {
            return new DirectoryCopier(directory);
        }

        public DirectoryCopier To(string destionationPah)
        {
            CopyAll(_directory, destionationPah);
            return this;
        }

        private static void CopyAll(DirectoryInfo directory, string destinationPath)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
            foreach(var file in directory.GetFiles())
            {
                file.CopyTo(Path.Combine(destinationPath, file.Name));
            }
            foreach(var subDir in directory.GetDirectories())
            {
                CopyAll(subDir, Path.Combine(destinationPath, subDir.Name));
            }
        }
    }
}
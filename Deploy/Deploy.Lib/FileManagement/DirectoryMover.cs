using System.IO;

namespace Deploy.Lib.FileManagement
{
    public class DirectoryMover
    {
        private readonly DirectoryInfo _directory;
        private readonly bool _directoryIncluded;

        private DirectoryMover(DirectoryInfo directory, bool directoryIncluded)
        {
            _directory = directory;
            _directoryIncluded = directoryIncluded;
        }

        public static DirectoryMover MoveContentsOf(DirectoryInfo directoryInfo)
        {
            return new DirectoryMover(directoryInfo, false);
        }

        public static DirectoryMover Move(DirectoryInfo directoryInfo)
        {
            return new DirectoryMover(directoryInfo, true);
        }

        public DirectoryMover To(string destinationPath)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
            if (_directoryIncluded)
            {
                _directory.MoveTo(Path.Combine(destinationPath, _directory.Name));
            }
            else
            {
                foreach(var subDir in _directory.GetDirectories())
                {
                    subDir.MoveTo(Path.Combine(destinationPath, subDir.Name));
                }
                foreach(var file in _directory.GetFiles())
                {
                    file.MoveTo(Path.Combine(destinationPath, file.Name));
                }
            }
            return this;
        }
    }
}
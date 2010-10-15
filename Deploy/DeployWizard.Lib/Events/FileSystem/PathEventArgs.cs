namespace DeployWizard.Lib.Events.FileSystem
{
    public class PathEventArgs
    {
        public string Path { get; private set; }

        public PathEventArgs(string path)
        {
            Path = path;
        }
    }
}
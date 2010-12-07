using DeployWizard.Lib.Events.Connections;
using DeployWizard.Lib.Events.FileSystem;

namespace DeployWizard.Lib.Steps.Views
{
    public interface IWizardEvents
    {
        event CreateDirectoryEvent CreateDirectory;

    }

    public delegate void CreateDirectoryEvent(object sender, PathEventArgs args);
    public delegate void TestConnectionEvent(object sender, ConnectionEventArgs args);
}
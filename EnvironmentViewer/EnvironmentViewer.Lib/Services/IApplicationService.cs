using EnvironmentViewer.Lib.Domain;

namespace EnvironmentViewer.Lib.Services
{
    public interface IApplicationService
    {
        ApplicationState GetApplicationState(EnvironmentData environmentData);
    }
}
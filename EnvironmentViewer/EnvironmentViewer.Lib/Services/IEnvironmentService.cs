using EnvironmentViewer.Lib.Domain;

namespace EnvironmentViewer.Lib.Services
{
    public interface IEnvironmentService
    {
        EnvironmentState GetStateOf(EnvironmentData environmentData);
    }
}
using EnvironmentViewer.Lib.Domain;

namespace EnvironmentViewer.Lib.Services
{
    public interface IDatabaseService
    {
        DatabaseState GetDatabaseState(EnvironmentData environmentData);
    }
}
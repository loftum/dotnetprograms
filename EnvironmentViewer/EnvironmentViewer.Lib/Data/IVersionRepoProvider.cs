using EnvironmentViewer.Lib.SessionFactories;

namespace EnvironmentViewer.Lib.Data
{
    public interface IVersionRepoProvider
    {
        IVersionRepo GetVersionRepo(DatabaseCredentials credentials);
    }
}
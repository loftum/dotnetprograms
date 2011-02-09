using EnvironmentViewer.Lib.SessionFactories;

namespace EnvironmentViewer.Lib.Data
{
    public class VersionRepoProvider : IVersionRepoProvider
    {
        private readonly IVersionSessionFactoryProvider _sessionFactoryProvider;

        public VersionRepoProvider(IVersionSessionFactoryProvider sessionFactoryProvider)
        {
            _sessionFactoryProvider = sessionFactoryProvider;
        }

        public IVersionRepo GetVersionRepo(DatabaseCredentials credentials)
        {
            return new VersionRepo(_sessionFactoryProvider.Create(credentials));
        }
    }
}
using NHibernate;

namespace EnvironmentViewer.Lib.SessionFactories
{
    public interface IVersionSessionFactoryProvider
    {
        ISessionFactory Create(DatabaseCredentials credentials);
    }
}
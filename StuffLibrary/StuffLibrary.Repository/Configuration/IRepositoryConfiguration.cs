using NHibernate;

namespace StuffLibrary.Repository.Configuration
{
    public interface IRepositoryConfiguration
    {
        ISessionFactory CreateSessionFactory();
    }
}
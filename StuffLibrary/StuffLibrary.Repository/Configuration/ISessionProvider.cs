using NHibernate;

namespace StuffLibrary.Repository.Configuration
{
    public interface ISessionProvider
    {
        ISession GetCurrent();
    }
}
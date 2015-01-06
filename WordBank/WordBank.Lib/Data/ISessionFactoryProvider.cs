using NHibernate;

namespace Wordbank.Lib.Data
{
    public interface ISessionFactoryProvider
    {
        ISessionFactory CreateSessionFactory();
    }
}
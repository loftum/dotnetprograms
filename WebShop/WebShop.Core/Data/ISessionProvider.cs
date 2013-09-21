using NHibernate;

namespace WebShop.Core.Data
{
    public interface ISessionProvider
    {
        ISession CurrentSession { get; }
    }
}
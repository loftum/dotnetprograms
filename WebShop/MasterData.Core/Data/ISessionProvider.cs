using NHibernate;

namespace MasterData.Core.Data
{
    public interface ISessionProvider
    {
        ISession CurrentSession { get; }
    }
}
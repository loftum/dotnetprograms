using NHibernate;

namespace VisualFarmStudio.Core.DataAccess
{
    public interface ISessionProvider
    {
        ISession GetSession();
    }
}
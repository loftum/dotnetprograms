using NHibernate;

namespace VisualFarmStudio.Lib.DataAccess
{
    public interface ISessionProvider
    {
        ISession GetSession();
    }
}
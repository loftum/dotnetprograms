using NHibernate;

namespace BasicManifest.Data.Setup
{
    public interface ISessionProvider
    {
        ISession GetSession();
    }
}
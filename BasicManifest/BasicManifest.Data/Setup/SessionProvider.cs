using BasicManifest.Common.Configuration;
using NHibernate;

namespace BasicManifest.Data.Setup
{
    public class SessionProvider : ISessionProvider
    {
        private static readonly ISessionFactory Factory;

        static SessionProvider()
        {
            Factory = SessionFactoryBuilder.BuildSessionFactory(new AuditEventListener(), new BMConfig());
        }
        
        private ISession _session;

        public ISession GetSession()
        {
            if (_session == null || !_session.IsOpen)
            {
                _session = Factory.OpenSession();
            }
            return _session;
        }
    }
}
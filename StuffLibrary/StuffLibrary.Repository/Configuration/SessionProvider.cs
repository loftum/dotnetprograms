using NHibernate;

namespace StuffLibrary.Repository.Configuration
{
    public class SessionProvider : ISessionProvider
    {
        private readonly ISession _session;

        public SessionProvider(IRepositoryConfiguration configuration)
        {
            var factory = configuration.CreateSessionFactory();
            var session = factory.GetCurrentSession();
            if (session == null || !session.IsOpen)
            {
                session = factory.OpenSession();
            }
            _session = session;
        }

        public ISession GetCurrent()
        {
            return _session;
        }
    }
}
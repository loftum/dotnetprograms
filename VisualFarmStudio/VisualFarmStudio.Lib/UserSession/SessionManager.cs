namespace VisualFarmStudio.Lib.UserSession
{
    public class SessionManager : ISessionManager
    {
        public const string UserContextKey = "UserContext";
        
        private readonly ISession _session;

        public UserContext UserContext
        {
            get { return _session.Read<UserContext>(UserContextKey); }
            set { _session.Write(UserContextKey, value); }
        }
        
        public SessionManager(ISession session)
        {
            _session = session;
        }

        public void Abandon()
        {
            _session.Abandon();
        }
    }
}
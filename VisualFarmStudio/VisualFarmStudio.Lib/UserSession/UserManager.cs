using System.Web.Security;
using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Lib.UserSession
{
    public class UserManager : IUserManager
    {
        private readonly ISessionManager _sessionManager;

        public UserManager(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public UserContext CurrentUser
        {
            get { return _sessionManager.UserContext; }
        }

        public bool IsLoggedIn {get { return CurrentUser != null; }}
        

        public void LogIn(BondeModel bonde)
        {
            _sessionManager.UserContext = new UserContext(bonde);
            FormsAuthentication.SetAuthCookie(bonde.Brukernavn, true);
        }

        public void LogOut()
        {
            _sessionManager.UserContext = null;
            FormsAuthentication.SignOut();
            _sessionManager.Abandon();
        }
    }
}
using System.Web;
using VisualFarmStudio.Lib.UserSession;

namespace VisualFarmStudio.ExtensionMethods
{
    public static class HttpSessionStateExtensions
    {
        public static UserContext GetUser(this HttpSessionStateBase session)
        {
            return (UserContext) session[SessionManager.UserContextKey];
        }
    }
}
using System.Web;
using System.Web.SessionState;

namespace VisualFarmStudio.Lib.UserSession
{
    public class HttpSession : ISession
    {
        private static HttpSessionState Session { get { return HttpContext.Current.Session; } }

        public void Write(string key, object value)
        {
            if (value == null)
            {
                Session.Remove(key);
            }
            else
            {
                Session[key] = value;    
            }
        }

        public T Read<T>(string key)
        {
            return (T) Session[key];
        }

        public void Abandon()
        {
            Session.Abandon();
        }
    }
}
using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.SessionState;
using DotNetPrograms.Common.ExtensionMethods;

namespace WebShop.Core.Users
{
    public class HttpUserSession : IUserSession
    {
        private static HttpSessionState Session { get { return HttpContext.Current.Session; } }

        public User User
        {
            get { return Get(() => User); }
            set { Set(() => User, value); }
        }

        public HttpUserSession()
        {
            if (User == null)
            {
                User = new User();
            }
        }

        private static T Get<T>(Expression<Func<T>> property) where T : class
        {
            return (T)Session[property.GetMemberName()];
        }

        private static void Set<T>(Expression<Func<T>> property, T item) where T : class
        {
            var name = property.GetMemberName();
            if (item == null)
            {
                Session.Remove(name);
            }
            Session[name] = item;
        }
    }
}
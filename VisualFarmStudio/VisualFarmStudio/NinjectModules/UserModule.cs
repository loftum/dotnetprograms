using Ninject.Modules;
using VisualFarmStudio.Common.Scoping;
using VisualFarmStudio.Lib.UserSession;

namespace VisualFarmStudio.NinjectModules
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISession>().To<HttpSession>().InScope(c => InjectionScope.Current);
            Bind<ISessionManager>().To<SessionManager>().InScope(c => InjectionScope.Current);
            Bind<IUserManager>().To<UserManager>().InScope(c => InjectionScope.Current);
        }
    }
}
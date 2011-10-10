using DbTool.Lib.Configuration;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace DbTool.Lib.Modules
{
    public class ConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbToolConfig>().To<DbToolConfig>().InSingletonScope();
            Bind<IDbToolSettings>().ToMethod(GetSettings);
        }

        private static IDbToolSettings GetSettings(IContext context)
        {
            return context.Kernel.Get<IDbToolConfig>().Settings;
        }
    }
}
using DbTool.Lib.Configuration;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace DbToolGui.Modules
{
    public class SettingsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbToolConfig>().To<DbToolConfig>().InSingletonScope();
            Bind<IDbToolSettings>().ToMethod(GetSettings);
        }

        private static IDbToolSettings GetSettings(IContext arg)
        {
            return arg.Kernel.Get<IDbToolConfig>().Settings;
        }
    }
}
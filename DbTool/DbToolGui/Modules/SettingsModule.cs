using DbTool.Lib.Configuration;
using DbTool.Lib.Ui.Worksheet;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace DbToolGui.Modules
{
    public class SettingsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorksheetManager>().To<WorksheetManager>().InSingletonScope();
            Bind<IDbToolConfig>().To<DbToolConfig>().InSingletonScope();
            Bind<IDbToolSettings>().ToMethod(GetSettings);
        }

        private static IDbToolSettings GetSettings(IContext arg)
        {
            return arg.Kernel.Get<IDbToolConfig>().Settings;
        }
    }
}
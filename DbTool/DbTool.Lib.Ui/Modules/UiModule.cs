using DbTool.Lib.Syntax;
using Ninject.Modules;

namespace DbTool.Lib.Ui.Modules
{
    public class UiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMetaInfoProvider>().To<MetaInfoProvider>().InSingletonScope();
        }
    }
}


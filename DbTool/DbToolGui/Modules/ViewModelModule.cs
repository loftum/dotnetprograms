using DbTool.Lib.Objects;
using DbTool.Lib.Ui.Highlighting;
using DbTool.Lib.Ui.Syntax;
using DbToolGui.Highlighting;
using Ninject.Modules;

namespace DbToolGui.Modules
{
    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IObjectCache>().To<DbToolObjectCache>().InSingletonScope();
            Bind<ISyntaxProvider>().To<DbToolSyntaxProvider>().InSingletonScope();
            Bind<ISyntaxParser>().To<DbToolSyntaxParser>().InSingletonScope();
            Bind<IMetaInfoProvider>().To<MetaInfoProvider>().InSingletonScope();
        }
    }
}
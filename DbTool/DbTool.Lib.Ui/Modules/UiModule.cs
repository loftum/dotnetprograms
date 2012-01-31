using Ninject.Modules;
using DbTool.Lib.Ui.Syntax;

namespace DbTool.Lib.Ui.Modules
{
    public class UiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISchemaObjectProvider>().To<SchemaObjectProvider>().InSingletonScope();
        }
    }
}


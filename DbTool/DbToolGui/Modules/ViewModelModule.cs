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
            Bind<ISyntaxProvider>().To<DbToolSyntaxProvider>().InSingletonScope();
            Bind<ISyntaxParser>().To<DbToolSyntaxParser>().InSingletonScope();
            Bind<ISchemaObjectProvider>().To<SchemaObjectProvider>().InSingletonScope();
        }
    }
}
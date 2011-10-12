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
            Bind<ISyntaxHighlighterFactory>().To<SyntaxHighlighterFactory>().InSingletonScope();
            Bind<ISchemaObjectProvider>().To<SchemaObjectProvider>().InSingletonScope();
        }
    }
}
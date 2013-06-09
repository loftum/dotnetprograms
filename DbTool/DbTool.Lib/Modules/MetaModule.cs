using DbTool.Lib.CSharp;
using DbTool.Lib.CSharp.Mono;
using DbTool.Lib.Communication.DbCommands.CSharp;
using DbTool.Lib.Meta;
using Ninject.Modules;

namespace DbTool.Lib.Modules
{
    public class MetaModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDatabaseToAssemblyConverter>().To<DatabaseToAssemblyConverter>().InSingletonScope();
            Bind<ITypeCache>().To<DbToolTypeCache>().InSingletonScope();
            Bind<ICSharpEvaluator>().To<MonoCSharpEvaluator>().InSingletonScope();
            Bind<ICSharpExecutor>().To<CSharpExecutor>().InSingletonScope();
        }
    }
}
using DbTool.Lib.Meta;
using Ninject.Modules;

namespace DbTool.Lib.Modules
{
    public class MetaModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITypeCache>().To<DbToolTypeCache>().InSingletonScope();
        }
    }
}
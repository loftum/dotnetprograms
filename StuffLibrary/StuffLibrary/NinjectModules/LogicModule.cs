using System.Reflection;
using Ninject.Modules;
using StuffLibrary.Common.Scoping;
using StuffLibrary.Lib.BusinessLogic;

namespace StuffLibrary.NinjectModules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            foreach (var type in Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location).GetExportedTypes())
            {
                Bind(type.GetType()).ToSelf().InRetainableRequestScope();
            }
            Bind<IMovieLogic>().To<MovieLogic>().InRetainableRequestScope();
        }
    }
}
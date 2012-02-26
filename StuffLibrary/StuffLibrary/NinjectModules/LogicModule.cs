using System.Reflection;
using Ninject.Modules;
using StuffLibrary.Common.Scoping;
using StuffLibrary.Lib.BusinessLogic;
using StuffLibrary.Lib.UnitOfWork;

namespace StuffLibrary.NinjectModules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            foreach (var type in Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location).GetExportedTypes())
            {
                Bind(type.GetType()).ToSelf().InCurrentInjectionScope();
            }
            Bind<IUnitOfWork>().To<NHibernateUnitOfWork>().InCurrentInjectionScope();
            Bind<IMovieLogic>().To<MovieLogic>().InCurrentInjectionScope();
            Bind<ICategoryLogic>().To<CategoryLogic>().InCurrentInjectionScope();
        }
    }
}
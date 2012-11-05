using DotNetPrograms.Common.Web.Reading;
using Ninject.Modules;
using StuffLibrary.Common.Configuration;
using StuffLibrary.Common.ExtensionMethods;
using StuffLibrary.Core.Facades;
using StuffLibrary.Core.RottenTomatoes;

namespace StuffLibrary.Web.NinjectModules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStuffLibrarySettings>().To<StuffLibrarySettings>().InCurrentScope();
            Bind<IRottenTomatoesService>().To<RottenTomatoesService>().InCurrentScope();
            Bind<IMovieFacade>().To<MovieFacade>().InCurrentScope();
            Bind<IHttpReader>().To<HttpReader>().InCurrentScope();
        }
    }
}
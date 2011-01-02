using HourGlass.Lib.Data;
using HourGlass.Lib.DateAndTime;
using HourGlass.Lib.Services;
using NHibernate;
using Ninject.Modules;

namespace HourGlass.Lib.Modules
{
    public class RepoModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDateProvider>().To<DateProvider>().InSingletonScope();
            Bind<ISessionFactory>()
                .ToMethod(context =>
                    SessionFactoryProvider.SqliteSessionFactory("HourGlass.db")).InSingletonScope();
            Bind<IHourGlassRepo>().To<HourGlassRepo>().InSingletonScope();
            
        }
    }
}
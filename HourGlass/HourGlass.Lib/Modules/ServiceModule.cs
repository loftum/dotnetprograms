using HourGlass.Lib.Services;
using Ninject.Modules;

namespace HourGlass.Lib.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHourCodeService>().To<HourCodeService>().InSingletonScope();
            Bind<IWeekService>().To<WeekService>().InSingletonScope();
        }
    }
}
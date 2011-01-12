using HourGlass.Providers;
using HourGlass.ViewModels;
using Ninject.Modules;

namespace HourGlass.Modules
{
    public class HourGlassModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHourCodeProvider>().To<HourCodeProvider>().InSingletonScope();
            Bind<IWeeksViewModel>().To<WeeksViewModel>().InSingletonScope();
            Bind<IHourCodesViewModel>().To<HourCodesViewModel>().InSingletonScope();
            Bind<IHourGlassViewModel>().To<HourGlassViewModel>().InSingletonScope();
        }
    }
}
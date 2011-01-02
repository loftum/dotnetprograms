using HourGlass.ViewModels;
using Ninject.Modules;

namespace HourGlass.Modules
{
    public class HourGlassModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHourGlassViewModel>().To<HourGlassViewModel>().InSingletonScope();
        }
    }
}
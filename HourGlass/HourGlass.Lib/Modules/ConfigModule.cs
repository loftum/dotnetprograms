using HourGlass.Lib.Configurating;
using Ninject.Modules;

namespace HourGlass.Lib.Modules
{
    public class ConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHourGlassConfig>().To<HourGlassConfig>().InSingletonScope();
        }
    }
}
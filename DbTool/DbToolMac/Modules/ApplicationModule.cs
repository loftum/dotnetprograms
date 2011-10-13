using System;
using Ninject.Modules;
using Ninject.Activation;
using MonoMac.AppKit;
using DbToolMac.Delegation;
using Ninject;

namespace DbToolMac.Modules
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbToolControllerDelegate>().To<DbToolControllerDelegate>().InSingletonScope();
            Bind<MainWindowController>().ToMethod(CreateController).InSingletonScope();
        }

        private static MainWindowController CreateController(IContext context)
        {
            var controllerDelegate = context.Kernel.Get<IDbToolControllerDelegate>();
            return new MainWindowController(controllerDelegate);
        }
    }
}


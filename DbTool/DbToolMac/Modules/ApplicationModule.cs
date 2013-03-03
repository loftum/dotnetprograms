using Ninject.Modules;
using Ninject.Activation;
using DbTool.Lib.Connections;
using DbTool.Lib.Communication;
using DbTool.Lib.Configuration;
using DbTool.Lib.Syntax;
using Ninject;
using DbTool.Lib.Meta;

namespace DbToolMac.Modules
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindowController>().ToMethod(CreateController).InSingletonScope();
        }

        private static MainWindowController CreateController(IContext context)
        {
            var kernel = context.Kernel;

			var settings = kernel.Get<IDbToolSettings>();
			var communicator = kernel.Get<IDatabaseCommunicator>();
			var typeCache = kernel.Get<ITypeCache>();

            return new MainWindowController(communicator, settings, typeCache);
        }
    }
}


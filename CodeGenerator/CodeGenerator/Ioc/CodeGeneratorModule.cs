using Caliburn.Micro;
using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace CodeGenerator.Ioc
{
    public class CodeGeneratorModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(s => s
                .FromThisAssembly()
                .SelectAllClasses().BindAllInterfaces()
                .Configure(b => b.InTransientScope()));

            Kernel.Bind(s => s
                .From("CodeGenerator.Lib")
                .SelectAllClasses().BindAllInterfaces()
                .Configure(b => b.InTransientScope()));
            Bind<IWindowManager>().To<CodeGeneratorWindowManager>();
        }
    }
}
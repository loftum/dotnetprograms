using System;
using System.Collections.Generic;
using Caliburn.Micro;
using CodeGenerator.Ioc;
using CodeGenerator.ViewModels;
using Ninject;

namespace CodeGenerator
{
    public class GeneratorBootstrapper : Bootstrapper<GeneratorViewModel>
    {
        private IKernel _kernel;

        protected override void Configure()
        {
            _kernel = new StandardKernel(new CodeGeneratorModule());
        }

        protected override object GetInstance(Type service, string key)
        {
            try
            {
                return _kernel.Get(service);
            }
            catch (ActivationException)
            {

                return base.GetInstance(service, key);
            }
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }
    }
}
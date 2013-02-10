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
            catch (ActivationException ex)
            {
                try
                {
                    return base.GetInstance(service, key);
                }
                catch
                {
                    throw new ActivationException(string.Format("Could not get instance of {0} (key='{1}')", service.Name, key), ex);
                }
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
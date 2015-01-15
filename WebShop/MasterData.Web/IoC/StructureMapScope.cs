using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace MasterData.Web.IoC
{
    public class StructureMapScope : ServiceLocatorImplBase, IDependencyScope
    {
        protected readonly IContainer Container;

        public StructureMapScope(IContainer container)
        {
            Container = container;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return DoGetAllInstances(serviceType);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                           ? Container.TryGetInstance(serviceType)
                           : Container.GetInstance(serviceType);
            }
            return Container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            if (Container != null)
            {
                Container.Dispose();
            }
        }
    }
}
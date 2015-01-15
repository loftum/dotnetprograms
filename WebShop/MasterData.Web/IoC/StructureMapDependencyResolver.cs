using System.Web.Http.Dependencies;
using StructureMap;

namespace MasterData.Web.IoC
{
    public class StructureMapDependencyResolver : StructureMapScope, IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            return new StructureMapScope(Container.GetNestedContainer());
        }
    }
}
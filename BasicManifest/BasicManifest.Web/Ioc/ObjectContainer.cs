using DotNetPrograms.Common.ExtensionMethods;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace BasicManifest.Web.Ioc
{
    public static class ObjectContainer
    {
        public static void Init(Registry registry, params Registry[] additionalRegistries)
        {
            var registries = registry.ToListWith(additionalRegistries);
            ObjectFactory.Initialize(a =>
                {
                    foreach (var r in registries)
                    {
                        a.AddRegistry(r);
                    }
                });
        }

        public static void Reset()
        {
            ObjectFactory.Initialize(DoNothing);
        }

        public static T Get<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }

        private static void DoNothing(IInitializationExpression e)
        {
        }
    }
}
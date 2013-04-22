using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Generate.Ioc
{
    public static class ObjectContainer
    {
        public static void Init(Registry registry, params Registry[] additionalRegistries)
        {
            var registries = new[] {registry}.Union(additionalRegistries);
            ObjectFactory.Configure(AddRegistries(registries));
        }

        private static Action<ConfigurationExpression> AddRegistries(IEnumerable<Registry> registries)
        {
            return a =>
                {
                    foreach (var registry in registries)
                    {
                        a.AddRegistry(registry);
                    }
                };
        }

        public static void Reset()
        {
            ObjectFactory.Configure(DoNothing);
        }

        private static void DoNothing(ConfigurationExpression expression)
        {
        }

        public static T Get<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }

        public static IList<T> GetAll<T>()
        {
            return ObjectFactory.GetAllInstances<T>();
        }
    }
}
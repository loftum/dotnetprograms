using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MongoTool.Core.CSharp
{
    public class AssemblySet : IEnumerable<Assembly>
    {
        private readonly IDictionary<string, Assembly> _assemblies = new Dictionary<string, Assembly>();

        public AssemblySet()
        {
        }

        public AssemblySet(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                _assemblies[assembly.GetName().Name] = assembly;
            }
        }

        public bool Contains(Assembly assembly)
        {
            return Contains(assembly.GetName());
        }

        public bool Contains(AssemblyName assemblyName)
        {
            return Contains(assemblyName.Name);
        }

        public bool Contains(string name)
        {
            return _assemblies.ContainsKey(name);
        }

        public void Add(Assembly assembly)
        {
            if (Contains(assembly))
            {
                throw new InvalidOperationException(string.Format("I already have {0}", assembly.GetName().FullName));
            }
            _assemblies[assembly.GetName().Name] = assembly;
        }

        public IEnumerator<Assembly> GetEnumerator()
        {
            return _assemblies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
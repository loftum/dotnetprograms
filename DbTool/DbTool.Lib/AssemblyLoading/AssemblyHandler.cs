using System;
using System.Linq;
using System.Reflection;
using DbTool.Lib.Exceptions;

namespace DbTool.Lib.AssemblyLoading
{
    public class AssemblyHandler
    {
        private readonly string _databaseType;
        private readonly Assembly _assembly;

        public AssemblyHandler(string databaseType, Assembly assembly)
        {
            _databaseType = databaseType;
            _assembly = assembly;
        }

        public T CreateInstance<T>(params object[] constructorArgs)
        {
            var expectedType = typeof(T);
            
            var type = _assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface)
                .FirstOrDefault();
            if (type == null)
            {
                throw new DbToolException("Could not find any {0} for databasetype {1} in assembly {2}",
                    expectedType.Name, _databaseType, _assembly.GetName());
            }

            return (T)Activator.CreateInstance(type, constructorArgs);
        }
    }
}
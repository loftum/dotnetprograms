using System;
using System.Collections.Generic;
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
            var implementingType = TryGetImplementingType<T>();
            if (implementingType == null)
            {
                throw new DbToolException("Could not find any {0} for databasetype {1} in assembly {2}",
                    typeof(T).Name, _databaseType, _assembly.GetName());
            }

            return (T)Activator.CreateInstance(implementingType, constructorArgs);
        }

        private Type TryGetImplementingType<T>()
        {
            var superType = typeof(T);
            try
            {
				foreach(var type in GetSubTypesOf<T>())
				{
					return type;
				}
                throw new DbToolException("Could not find any {0} for databasetype {1} in assembly {2}",
                    superType.Name, _databaseType, _assembly.GetName());
            }
            catch (ReflectionTypeLoadException)
            {
                throw new DbToolException("Could not find any {0} for databasetype {1} in assembly {2}",
                    superType.Name, _databaseType, _assembly.GetName());
            }
        }

        public bool HasType<T>()
        {
			foreach (var type in GetSubTypesOf<T>())
			{
				return true;
			}
			return false;
        }

        private Type[] GetSubTypesOf<T>()
        {
            return _assembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface).ToArray();
        }
    }
}
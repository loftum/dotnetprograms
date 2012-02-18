using System;
using System.Collections.Generic;
using System.Linq;

namespace DbTool.Lib.Objects.TypeCaching
{
    public abstract class TypeCache
    {
        private readonly bool _caseSensitive;

        protected TypeCache(bool caseSensitive)
        {
            _caseSensitive = caseSensitive;
        }

        protected readonly IDictionary<Type, ISet<string>> Names = new Dictionary<Type, ISet<string>>();
        
        public void Add(DbToolObject obj)
        {
            var name = _caseSensitive ? obj.Name : obj.Name.ToLowerInvariant();
            NamesForType(obj.GetType()).Add(name);
        }

        public Type GetTypeForName(string typeName)
        {
            var name = _caseSensitive ? typeName : typeName.ToLowerInvariant();
            return Names.Keys.Where(type => Names[type].Contains(name)).FirstOrDefault();
        }

        protected ISet<string> NamesForType(Type type)
        {
            try
            {
                return Names[type];
            }
            catch (KeyNotFoundException)
            {
                var names = new HashSet<string>();
                Names[type] = names;
                return names;
            }
        }

        public void Clear()
        {
            Names.Clear();
        }
    }
}
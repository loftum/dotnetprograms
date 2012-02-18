using System;
using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Objects.TypeCaching;

namespace DbTool.Lib.Objects
{
    public abstract class ObjectContainerBase<TNameSpace, TTypeCache> : IObjectContainer
        where TNameSpace : NameSpace<TTypeCache> where TTypeCache : TypeCache, new()
    {
        protected readonly IDictionary<string, TNameSpace> Namespaces = new Dictionary<string, TNameSpace>();

        public void AddObject(DbToolObject obj)
        {
            var nameSpace = GetOrCreateNameSpace(obj.NameSpace);
            nameSpace.Add(obj);
        }

        public Type GetObjectType(string name)
        {
            return Namespaces.Values.Select(nameSpace => nameSpace.GetObjectType(name)).FirstOrDefault(type => type != null);
        }

        public DbToolObject GetObject(string name)
        {
            DbToolObject obj = null;
            return Namespaces.Values.Any(nameSpace => nameSpace.TryGet(name, out obj)) ? obj : obj;
        }

        public void Clear()
        {
            Namespaces.Clear();
        }

        public IEnumerable<DbToolObject> GetAllObjects()
        {
            return Namespaces.Values.SelectMany(nameSpace => nameSpace.Objects);
        }

        public TNameSpace GetOrCreateNameSpace(string name)
        {
            var nameSpace = GetNameSpace(name);
            if (nameSpace == null)
            {
                nameSpace = CreateNameSpace(name);
                Namespaces[name] = nameSpace;
            }
            return nameSpace;
        }

        public TNameSpace GetNameSpace(string name)
        {
            try
            {
                return Namespaces[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public abstract TNameSpace CreateNameSpace(string name);
    }
}
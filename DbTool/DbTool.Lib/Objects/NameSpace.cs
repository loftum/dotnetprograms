using System;
using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Objects.TypeCaching;

namespace DbTool.Lib.Objects
{
    public abstract class NameSpace<TTypeCache> where TTypeCache : TypeCache, new()
    {
        private readonly TTypeCache _typeCache = new TTypeCache();
        public string Name { get; private set; }
        public IEnumerable<DbToolObject> Objects { get { return _objectDictionary.Values.ToList(); } }
        private readonly IDictionary<string, DbToolObject> _objectDictionary = new Dictionary<string, DbToolObject>();

        protected NameSpace(string name)
        {
            Name = name;
        }

        public bool TryGet(string name, out DbToolObject dbToolObject)
        {
            dbToolObject = Get(name);
            return dbToolObject != null;
        }

        protected void DoAdd(string name, DbToolObject obj)
        {
            _objectDictionary[name] = obj;
            _typeCache.Add(obj);
        }

        protected DbToolObject DoGet(string name)
        {
            try
            {
                return _objectDictionary[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public Type GetObjectType(string name)
        {
            return _typeCache.GetTypeForName(name);
        }

        public abstract DbToolObject Get(string name);
        public abstract void Add(DbToolObject obj);
    }
}
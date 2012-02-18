using System;
using DbTool.Lib.Objects.CSharp;
using DbTool.Lib.Objects.Database;

namespace DbTool.Lib.Objects
{
    public class DbToolObjectCache : IObjectCache
    {
        private readonly object _schemaLock = new object();
        private SchemaObjectContainer _schema;
        public SchemaObjectContainer Schema
        {
            get
            {
                lock (_schemaLock)
                {
                    return _schema;
                }
            }
            set
            {
                lock(_schemaLock)
                {
                    _schema = value;
                }
            }
        }
        public CSharpObjectContainer CSharpObjects { get; set; }

        public DbToolObjectCache()
        {
            Schema = new SchemaObjectContainer();
            CSharpObjects = new CSharpObjectContainer();
        }

        public DbToolObject GetObject(string name)
        {
            return Schema != null ? Schema.GetObject(name) : null;
        }

        public Type GetObjectType(string name)
        {
            return Schema.GetObjectType(name);
        }
    }
}
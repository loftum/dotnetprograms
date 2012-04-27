using System.Collections.Generic;

namespace DbTool.Lib.Meta.Types
{
    public class TypeContainer
    {
        private readonly bool _caseSensitive;
        public string Name { get; private set; }
        private readonly IDictionary<string, TypeMeta> _types = new Dictionary<string, TypeMeta>();

        public TypeContainer(string name, bool caseSensitive = false)
        {
            Name = name;
            _caseSensitive = caseSensitive;
        }

        public TypeMeta Get(string name)
        {
            var typeName = _caseSensitive ? name : name.ToLowerInvariant();
            return _types.ContainsKey(typeName) ? _types[typeName] : null;
        }

        public TypeMeta Add(TypeMeta typeMeta)
        {
            var typeName = _caseSensitive ? typeMeta.TypeName : typeMeta.TypeName.ToLowerInvariant();
            _types[typeName] = typeMeta;
            return typeMeta;
        }

        public TypeMeta GetOrAdd(TypeMeta tableMeta)
        {
            var existing = Get(tableMeta.TypeName);
            return existing ?? Add(tableMeta);
        }
    }
}
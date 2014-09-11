using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DataAccess.Sql.Mapping
{
    public class AnonymousTypeMapper : TypeMapper
    {
        private readonly PropertyInfo[] _properties;
        private readonly ConstructorInfo _ctor;

        public AnonymousTypeMapper(Type type) : base(type)
        {
            _properties = type.GetProperties();
            _ctor = type.GetConstructor(_properties.Select(p => p.PropertyType).ToArray());
        }

        public override object Convert(IDataRecord row)
        {
            var values = _properties.Select(p => Sanitize(row[p.Name])).ToArray();
            return _ctor.Invoke(values);
        }
    }
}
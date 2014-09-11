using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using DataAccess.Sql.ExtensionMethods;

namespace DataAccess.Sql.Mapping
{
    public class RegularTypeMapper : TypeMapper
    {
        private readonly IDictionary<string, PropertyInfo> _propertyMap = new Dictionary<string, PropertyInfo>();

        public RegularTypeMapper(Type type) : base(type)
        {
            var properties = type.GetProperties().Where(p => p.HasSetter()).ToArray();
            foreach (var property in properties)
            {
                _propertyMap[property.Name] = property;
            }
        }

        public override object Convert(IDataRecord row)
        {
            return Type.IsValueOrString()
                ? row[0]
                : ComplexObjectFor(row);
        }

        private object ComplexObjectFor(IDataRecord row)
        {
            var item = Activator.CreateInstance(Type);
            for (var ii = 0; ii < row.FieldCount; ii++)
            {
                var name = row.GetName(ii);
                var property = GetProperty(name);
                if (property != null)
                {
                    var value = Sanitize(row[ii]);
                    property.SetValue(item, value);
                }
            }
            return item;
        }

        private PropertyInfo GetProperty(string name)
        {
            return _propertyMap.ContainsKey(name) ? _propertyMap[name] : null;
        }
    }
}
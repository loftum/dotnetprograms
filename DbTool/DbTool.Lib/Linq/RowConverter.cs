using System;
using System.Collections.Generic;
using System.Data;
using DotNetPrograms.Common.Meta;

namespace DbTool.Lib.Linq
{
    public class RowConverter
    {
        private readonly TypeMeta _type;
        private readonly IList<string> _properties = new List<string>();

        public RowConverter(Type type)
        {
            _type = new TypeMeta(type);
            foreach (var property in _type.Properties)
            {
                _properties.Add(property.Name);
            }
        }

        public object Convert(IDataRecord row)
        {
            var item = Activator.CreateInstance(_type.Type);
            var reflector = new ObjectReflector(item);
            foreach (var property in _properties)
            {
                var value = Sanitize(row[property]);
                reflector.GetProperty(property).Value = value;
            }
            return item;
        }

        private static object Sanitize(object value)
        {
            return value == DBNull.Value ? null : value;
        }
    }
}
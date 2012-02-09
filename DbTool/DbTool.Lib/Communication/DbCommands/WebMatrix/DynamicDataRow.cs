using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace DbTool.Lib.Communication.DbCommands.WebMatrix
{
    public class DynamicDataRow : DynamicObject, ICustomTypeDescriptor
    {
        private readonly IDictionary<string, object> _dictionary;
        private readonly IList<object> _values; 
        public IList<string> Columns { get; private set; }

        public DynamicDataRow(IEnumerable<string> columnNames, DataRow dataRow)
        {
            Columns = columnNames.ToList();
            _dictionary = new Dictionary<string, object>();
            _values = new List<object>();
            var index = 0;
            foreach (var columnName in columnNames)
            {
                _dictionary[columnName.ToLowerInvariant()] = dataRow[columnName];
                _dictionary[columnName.Replace("_", string.Empty)] = dataRow[columnName];
                _values.Add(dataRow[index]);
                index++;
            }
        }

        public object this[string columnName]
        {
            get { return _dictionary[columnName.ToLowerInvariant()]; }
        }

        public object this[int index]
        {
            get { return _values[index]; }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this[binder.Name];
            return true;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Columns;
        }

        public AttributeCollection GetAttributes()
        {
            return AttributeCollection.Empty;
        }

        public string GetClassName()
        {
            return null;
        }

        public string GetComponentName()
        {
            return null;
        }

        public TypeConverter GetConverter()
        {
            return null;
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents()
        {
            return EventDescriptorCollection.Empty;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return EventDescriptorCollection.Empty;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            var descriptors = from columnName in Columns
                let value = _dictionary[columnName]
                let type = value.GetType()
                select new DynamicDataRowPropertyDescriptor(columnName, type);
            return new PropertyDescriptorCollection(descriptors.ToArray(), readOnly: true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }
}
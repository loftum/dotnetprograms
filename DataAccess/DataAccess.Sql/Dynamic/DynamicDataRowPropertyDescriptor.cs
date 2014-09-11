using System;
using System.ComponentModel;

namespace DataAccess.Sql.Dynamic
{
    public class DynamicDataRowPropertyDescriptor : PropertyDescriptor
    {
        private readonly Type _type;

        public DynamicDataRowPropertyDescriptor(string name, Type type)
            : base(name, new Attribute[0])
        {
            _type = type;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            var dynamicDataRow = component as DynamicDataRow;
            return dynamicDataRow == null ? null : dynamicDataRow[Name];
        }

        public override void ResetValue(object component)
        {

        }

        public override void SetValue(object component, object value)
        {

        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return typeof(DynamicDataRow); }
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override Type PropertyType
        {
            get { return _type; }
        } 
    }
}
namespace DotNetPrograms.Common.Meta
{
    public class PropertyReflector
    {
        private readonly object _item;
        private readonly PropertyMeta _property;

        public PropertyReflector(object item, PropertyMeta property)
        {
            _item = item;
            _property = property;
        }

        public object Value
        {
            set { _property.SetValue(_item, value); }
            get { return _property.GetValue(_item); }
        }
        public string Name { get { return _property.Name; } } 
    }
}
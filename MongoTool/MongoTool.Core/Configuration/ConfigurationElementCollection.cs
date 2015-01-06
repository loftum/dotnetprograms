using System;
using System.Collections.Generic;
using System.Configuration;

namespace MongoTool.Core.Configuration
{
    public abstract class ConfigurationElementCollection<T> : ConfigurationElementCollection, IEnumerable<T> where T : ConfigurationElement, new()
    {
        private readonly IList<T> _elements = new List<T>();

        private readonly string _elementName;

        protected override string ElementName
        {
            get { return _elementName; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        protected ConfigurationElementCollection(string elementName)
        {
            _elementName = elementName;
        }
        
        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(_elementName, StringComparison.InvariantCultureIgnoreCase);
        }


        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            _elements.Add((T)element);
        }

        protected abstract object GetElementKey(T element);

        protected override object GetElementKey(ConfigurationElement element)
        {
            return GetElementKey((T) element);
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
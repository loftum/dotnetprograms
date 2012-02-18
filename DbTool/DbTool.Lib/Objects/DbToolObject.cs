using System.Collections.Generic;

namespace DbTool.Lib.Objects
{
    public abstract class DbToolObject
    {
        public string FullName { get { return string.Join(".", NameSpace, Name); } }
        public string NameSpace { get; private set; }
        public string Name { get; private set; }

        public IEnumerable<DbToolProperty> Properties
        {
            get { return PropertyDictionary.Values; }
        }

        protected readonly IDictionary<string, DbToolProperty> PropertyDictionary;

        protected DbToolObject(string nameSpace, string name)
        {
            NameSpace = nameSpace;
            Name = name;
            PropertyDictionary = new Dictionary<string, DbToolProperty>();
        }

        public abstract void AddProperty(DbToolProperty property);

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as DbToolObject;
            if (other == null)
            {
                return false;
            }

            var thisType = GetType();
            var otherType = other.GetType();
            return (thisType == otherType && FullName.Equals(other.FullName));
        }
    }
}
using System;
using Convenient.ExtensionMethods;

namespace Convenient.Models
{
    public abstract class ObjectModel
    {
        public ObjectModel Parent { get; private set; }

        public virtual string FullName
        {
            get
            {
                if (Level <= 1)
                {
                    return Name;
                }
                return Parent is CollectionObjectModel
                    ? string.Format("{0}{1}", Parent.FullName, Name)
                    : string.Join(".", Parent.FullName, Name);
            }
        }

        public string FullDescriptor
        {
            get { return string.Join(" ", Type.GetFriendlyName(), FullName); }
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
        public ObjectType ObjectType { get; private set; }
        public Type Type { get; private set; }
        public int Level { get; private set; }
        protected ModelOptions Options;
        public ObjectTag Tag { get; private set; }

        protected ObjectModel(ObjectModel parent, string name, Type type, object value, ModelOptions options, ObjectTag tag = ObjectTag.Edit)
        {
            Parent = parent;
            Name = name;
            Value = value;
            Type = type;
            ObjectType = type.GetObjectType();
            Level = Parent == null ? 0 : Parent.Level + 1;
            Options = options;
            Tag = Parent == null ? tag : Options.GetOperationFor(parent.Type, name, tag);
        }

        protected ObjectModel For(ObjectModel parent, string name, Type type, object value, ObjectTag tag = ObjectTag.Edit)
        {
            var objectType = type.GetObjectType();
            switch (objectType)
            {
                case ObjectType.Simple:
                    return new SimpleObjectModel(parent, name, type, value, Options, tag);
                case ObjectType.Enum:
                    return new EnumObjectModel(parent, name, type, value, Options, tag);
                case ObjectType.Collection:
                    return new ListObjectModel(parent, name, type, value, Options, tag);
                case ObjectType.Dictionary:
                    return new DictionaryObjectModel(parent, name, type, value, Options, tag);
                default:
                    return new ComplexObjectModel(parent, name, type, value, Options, tag);
            }
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
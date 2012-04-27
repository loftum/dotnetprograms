using System;
using System.Collections.Generic;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta.Objects
{
    public abstract class ObjectMeta
    {
        public TypeMeta Type { get; protected set; }
        public string Name { get; private set; }
        public object Object { get; private set; }

        protected ObjectMeta(TypeMeta type, object obj, string name)
        {
            Object = obj;
            Type = type;
            Name = name;
        }

        public static ObjectMeta Null
        {
            get { return new NullObjectMeta();}
        }

        public abstract IEnumerable<ObjectMeta> Members { get; }
        public abstract IEnumerable<ObjectMeta> Properties { get; }

        public abstract ObjectMeta GetMember(string name);
        
        protected static void Safely(Action action)
        {
            try
            {
                action();
            }
            catch
            {
            }
        }

        public static ObjectMeta FromPath(ObjectMeta root, IEnumerable<string> path)
        {
            var current = root;
            foreach (var memberName in path)
            {
                var member = current.GetMember(memberName);
                if (member == null)
                {
                    throw new Exception(string.Format("Unknown member '{0}'", memberName));
                }
                current = member;
            }
            return current;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Type.TypeName, Name);
        }
    }
}
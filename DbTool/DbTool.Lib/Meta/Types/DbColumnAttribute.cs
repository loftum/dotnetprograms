using System;

namespace DbTool.Lib.Meta.Types
{
    public class DbColumnAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool IsNullable { get; private set; }

        public DbColumnAttribute(string name, string type) : this(name, type, false)
        {
        }

        public DbColumnAttribute(string name, string type, bool isNullable)
        {
            Name = name;
            Type = type;
            IsNullable = isNullable;
        }
    }
}
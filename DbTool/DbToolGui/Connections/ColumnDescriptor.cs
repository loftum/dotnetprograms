using System;

namespace DbToolGui.Connections
{
    public class ColumnDescriptor
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }

        public ColumnDescriptor(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
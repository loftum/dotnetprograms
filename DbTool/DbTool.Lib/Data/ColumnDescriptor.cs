using System;

namespace DbTool.Lib.Data
{
    public class ColumnDescriptor
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public Type Type { get; private set; }

        public ColumnDescriptor(int index, string name, Type type)
        {
            Index = index;
            Name = name;
            Type = type;
        }
    }
}
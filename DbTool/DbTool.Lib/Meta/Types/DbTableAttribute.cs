using System;

namespace DbTool.Lib.Meta.Types
{
    public class DbTableAttribute : Attribute
    {
        public string Name { get; private set; }

        public DbTableAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            Name = name;
        }
    }
}
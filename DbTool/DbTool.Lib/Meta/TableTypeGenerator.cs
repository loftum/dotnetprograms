using System;
using System.Reflection;
using DbTool.Lib.Communication.DbCommands.Dynamic;
using DbTool.Lib.Meta.Emit;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta
{
    [Serializable]
    public class TableTypeGenerator
    {
        public DynamicAssembly DynamicAssembly { get; private set; }

        public Assembly Assembly { get { return DynamicAssembly.Assembly; } }

        public TableTypeGenerator(string nameSpace)
        {
            DynamicAssembly = new DynamicAssembly(nameSpace);
        }

        public Type CreateType(TableMeta table)
        {
            var builder = DynamicAssembly.BuildClass(table.Name)
                .WithAttribute(() => new DbTableAttribute(table.Name))
                .WithAttribute<SerializableAttribute>();

            foreach (var column in table.Columns)
            {
                builder.AddProperty(column.Name, column.CSharpType);
            }

            var type = builder.CreateType();
            return type;
        }

        public static object Cast(DynamicDataRow record, Type type)
        {
            var generetedObject = Activator.CreateInstance(type);
            var properties = type.GetProperties();
            var propertiesCounter = 0;

            foreach (var column in record.Columns)
            {
                var value = record[column];
                properties[propertiesCounter].SetValue(generetedObject, value, null);
                propertiesCounter++;
            }
            return generetedObject;
        }
    }
}
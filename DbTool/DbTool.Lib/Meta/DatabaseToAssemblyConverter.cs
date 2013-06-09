using System;
using DbTool.Lib.Meta.Emit;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta
{
    public class DatabaseToAssemblyConverter : IDatabaseToAssemblyConverter
    {
        public DynamicAssembly CreateFor(DatabaseSchema schema)
        {
            var assembly = new DynamicAssembly(schema.FullName);
            foreach (var table in schema.Tables)
            {
                var builder = assembly.BuildClass(table.Name)
                        .WithAttribute<SerializableAttribute>();
                foreach (var column in table.Columns)
                {
                    builder.AddProperty(column.Name, column.CSharpType);
                }
                builder.CreateType();
            }
            return assembly;
        }
    }
}
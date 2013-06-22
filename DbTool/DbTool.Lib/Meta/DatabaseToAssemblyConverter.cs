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
                var builder = assembly.BuildClass(Typify(table.Name))
                                      .WithAttribute<SerializableAttribute>()
                                      .WithAttribute(() => new DbTableAttribute(table.Name));  
                foreach (var column in table.Columns)
                {
                    builder.WithProperty(Typify(column.Name), column.CSharpType, b => b
                        .WithGetter()
                        .WithSetter()
                        .WithAttribute(() => new DbColumnAttribute(column.Name, column.Type, column.IsNullable, column.IsPrimaryKey)));
                }
                builder.CreateType();
            }
            return assembly;
        }

        private static string Typify(string name)
        {
            return name.Replace(" ", "_");
        }
    }
}
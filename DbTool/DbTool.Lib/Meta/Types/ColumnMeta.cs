using System;
using System.Data;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Meta.Types
{
    public class ColumnMeta
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool IsNullable { get; private set; }
        public Type CSharpType { get; private set; }

        public ColumnMeta(DataRow row)
            : this(row.Get<string>("DATA_TYPE"), row.Get<string>("COLUMN_NAME"), row.Get<string>("IS_NULLABLE") == "YES")
        {
        }

        public ColumnMeta(string type, string name, bool isNullable)
        {
            Type = type;
            Name = name;
            IsNullable = isNullable;
            CSharpType = GetCSharpType();
        }

        private Type GetCSharpType()
        {
            switch (Type)
            {
                case "datetime":
                case "datetime2":
                    return TypeOf<DateTime>();
                case "bit":
                    return TypeOf<bool>();
                case "tinyint":
                    return TypeOf<byte>();
                case "smallint":
                    return TypeOf<short>();
                case "int":
                    return TypeOf<int>();
                case "bigint":
                    return TypeOf<long>();
                case "decimal":
                    return TypeOf<decimal>();
                case "uniqueidentifier":
                    return TypeOf<Guid>();
                case "varchar":
                    return typeof (string);
                case "nvarchar":
                    return typeof (string);
            }
            return typeof (object);
        }

        private Type TypeOf<T>() where T : struct
        {
            return IsNullable ? typeof (T?) : typeof (T);
        }
    }
}
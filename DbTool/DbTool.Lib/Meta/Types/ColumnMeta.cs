using System;
using System.Data;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Meta.Types
{
    public class ColumnMeta
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public Type CSharpType { get; private set; }

        public ColumnMeta(DataRow row)
            : this(row.Get<string>("DATA_TYPE"), row.Get<string>("COLUMN_NAME"))
        {
        }

        public ColumnMeta(string type, string name)
        {
            Type = type;
            Name = name;
            CSharpType = GetCSharpType();
        }

        private Type GetCSharpType()
        {
            switch (Type)
            {
                case "datetime":
                    return typeof (DateTime);
                case "bit":
                    return typeof (bool);
                case "tinyint":
                    return typeof (byte);
                case "smallint":
                    return typeof (short);
                case "int":
                    return typeof (int);
                case "bigint":
                    return typeof(long);
                case "decimal":
                    return typeof(decimal);
                case "uniqueidentifier":
                    return typeof (Guid);
                case "varchar":
                    return typeof (string);
                case "nvarchar":
                    return typeof (string);
            }
            return typeof (object);
        }
    }
}
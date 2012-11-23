using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Meta.Types
{
    public class ColumnMeta : TypeMeta
    {
        public Type CSharpType { get; private set; }

        public ColumnMeta(DataRow row)
            : base(row.Get<string>("DATA_TYPE"), row.Get<string>("COLUMN_NAME"))
        {
            CSharpType = GetCSharpType();
        }

        public ColumnMeta(string typeName, string memberName) : base(typeName, memberName)
        {
        }

        public override IEnumerable<TypeMeta> Members
        {
            get { return Enumerable.Empty<TypeMeta>(); }
        }

        public override IEnumerable<TypeMeta> Properties
        {
            get { return Enumerable.Empty<TypeMeta>(); }
        }

        private Type GetCSharpType()
        {
            switch (TypeName)
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
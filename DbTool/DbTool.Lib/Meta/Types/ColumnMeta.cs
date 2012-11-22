using System;
using System.Collections.Generic;
using System.Linq;

namespace DbTool.Lib.Meta.Types
{
    public class ColumnMeta : TypeMeta
    {
        public Type CSharpType { get; private set; }

        public ColumnMeta(string typeName, string columnName)
            : base(typeName, columnName)
        {
            CSharpType = GetCSharpType();
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
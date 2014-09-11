using System;

namespace DataAccess.Sql.Statements
{
    public class TypeAndTable
    {
        public Type Type { get; private set; }
        public string Table { get; private set; }

        public TypeAndTable(Type type, string table)
        {
            Type = type;
            Table = table;
        }

        public override bool Equals(object obj)
        {
            return Equals((TypeAndTable) obj);
        }

        protected bool Equals(TypeAndTable other)
        {
            return other != null &&
                   other.Type == Type &&
                   other.Table == Table;
        }

        public override int GetHashCode()
        {
            unchecked { return ((Type != null ? Type.GetHashCode() : 0) * 397) ^ (Table != null ? Table.GetHashCode() : 0); }
        }
    }
}
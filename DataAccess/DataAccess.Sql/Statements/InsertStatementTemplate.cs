using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using DataAccess.Sql.ExtensionMethods;

namespace DataAccess.Sql.Statements
{
    public class InsertStatementTemplate
    {
        private static readonly IDictionary<TypeAndTable, InsertStatementTemplate> Statements = new Dictionary<TypeAndTable, InsertStatementTemplate>();

        private readonly PropertyInfo[] _properties;
        private readonly TypeAndTable _typeAndTable;
        public string CommandText { get; private set; }

        private InsertStatementTemplate(TypeAndTable typeAndTable)
        {
            _typeAndTable = typeAndTable;
            _properties = typeAndTable.Type.GetProperties()
                .Where(p => p.GetCustomAttribute<GeneratedByDbAttribute>() == null)
                .ToArray();

            var columns = string.Join(", ", _properties.Select(p => p.Name.InBrackets()));
            var parameterNames = string.Join(", ", _properties.Select(p => p.Name.ToParameterName()));

            CommandText = string.Format("insert into [{0}] ({1}) values ({2})", typeAndTable.Table, columns, parameterNames);
        }

        public static InsertStatementTemplate For<T>()
        {
            return For(typeof (T));
        }

        public static InsertStatementTemplate For(Type type)
        {
            return For(type, type.Name);
        }

        public static InsertStatementTemplate For<T>(string table)
        {
            return For(typeof (T), table);
        }

        public static InsertStatementTemplate For(Type type, string table)
        {
            var key = new TypeAndTable(type, table);
            if (!Statements.ContainsKey(key))
            {
                Statements[key] = new InsertStatementTemplate(key);
            }
            return Statements[key];
        }

        public IEnumerable<SqlParameter> CreateParametersFor(object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (item.GetType() != _typeAndTable.Type)
            {
                throw new InvalidOperationException(string.Format("Item must be of type {0}, but is {1}", _typeAndTable.Type, item.GetType()));
            }
            return _properties.Select(p => new SqlParameter(p.Name.ToParameterName(), p.GetDbValue(item)));
        }
    }
}
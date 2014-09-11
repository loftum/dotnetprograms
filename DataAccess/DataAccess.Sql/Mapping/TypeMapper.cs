using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.Sql.ExtensionMethods;

namespace DataAccess.Sql.Mapping
{
    public abstract class TypeMapper
    {
        private static readonly IDictionary<Type, TypeMapper> TypeMappers = new Dictionary<Type, TypeMapper>();

        protected readonly Type Type;

        protected TypeMapper(Type type)
        {
            Type = type;
            
        }

        public static TypeMapper For(Type type)
        {
            if (!TypeMappers.ContainsKey(type))
            {
                TypeMappers[type] = CreateTypeMapperFor(type);
            }
            return TypeMappers[type];
        }

        private static TypeMapper CreateTypeMapperFor(Type type)
        {
            return type.IsAnonymous()
                    ? (TypeMapper) new AnonymousTypeMapper(type)
                    : new RegularTypeMapper(type);
        }

        public abstract object Convert(IDataRecord row);
        

        protected static object Sanitize(object value)
        {
            return value == DBNull.Value ? null : value;
        }
    }
}
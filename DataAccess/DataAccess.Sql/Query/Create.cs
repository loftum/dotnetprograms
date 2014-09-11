using System;
using System.Collections;
using System.Collections.Generic;
using DataAccess.Sql.ExtensionMethods;

namespace DataAccess.Sql.Query
{
    public class Create
    {
        public static IList ListOf(Type type)
        {
            return typeof(List<>).MakeGenericType(type).NewUpAs<IList>();
        }
    }
}
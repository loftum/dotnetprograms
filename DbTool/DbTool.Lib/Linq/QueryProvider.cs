using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq
{
    public class QueryProvider : IQueryProvider
    {
        private readonly Func<IQueryable, DbCommand> _translator;
        private readonly Func<Type, string, object[], IEnumerable> _executor;

        public QueryProvider(
            Func<IQueryable, SqlCommand> translator,
            Func<Type, string, object[], IEnumerable> executor)
        {
            _translator = translator;
            _executor = executor;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new Queryable<TElement>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var isCollection = typeof(TResult).IsGenericType &&typeof(TResult).GetGenericTypeDefinition() == typeof(IEnumerable<>);
            var itemType = isCollection
                ? typeof(TResult).GetGenericArguments().Single()
                : typeof(TResult);
            var queryable = Activator.CreateInstance(typeof(Queryable<>).MakeGenericType(itemType), this, expression) as IQueryable;

            IEnumerable queryResult;

            using (var command = _translator(queryable))
            {
                queryResult = _executor(
                    itemType,
                    command.CommandText,
                    command.Parameters.OfType<DbParameter>()
                                      .Select(parameter => parameter.Value)
                                      .ToArray());
            }

            return isCollection
                ? (TResult)queryResult
                : queryResult.OfType<TResult>()
                             .SingleOrDefault();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
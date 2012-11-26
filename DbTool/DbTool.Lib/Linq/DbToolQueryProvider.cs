using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq
{
    public class DbToolQueryProvider : IQueryProvider
    {
        private readonly IQueryableToSqlTranslator _translator;
        private readonly Func<Type, string, object[], IEnumerable> _executor;

        public DbToolQueryProvider(
            IQueryableToSqlTranslator translator,
            Func<Type, string, object[], IEnumerable> executor)
        {
            _translator = translator;
            _executor = executor;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new DbToolQueryable<TElement>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var isCollection = typeof(TResult).IsGenericType && typeof(TResult).GetGenericTypeDefinition() == typeof(IEnumerable<>);
            var itemType = isCollection
                ? typeof(TResult).GetGenericArguments().Single()
                : typeof(TResult);
            var queryable = (IQueryable) Activator.CreateInstance(typeof(DbToolQueryable<>).MakeGenericType(itemType), this, expression);

            IEnumerable queryResult;

            using (var command = _translator.Translate(queryable))
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
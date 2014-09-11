using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Sql.Linq.Statements;

namespace DataAccess.Sql.Query
{
    public class DataAccessQueryProvider : IQueryProvider, IDeleteProvider
    {
        private readonly Func<SqlQuery, Type, IEnumerable> _executeQuery;
        private readonly Func<ISqlStatement, int> _executeNonQuery;

        public DataAccessQueryProvider(Func<SqlQuery, Type, IEnumerable> executeQuery, Func<ISqlStatement, int> executeNonQuery)
        {
            _executeQuery = executeQuery;
            _executeNonQuery = executeNonQuery;
        }


        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new DataAccessQueryable<TElement>(this, expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {

            var isCollection = typeof (TResult).IsGenericType &&
                               typeof (TResult).GetGenericTypeDefinition() == typeof (IEnumerable<>);
            var itemType = isCollection
                ? typeof (TResult).GetGenericArguments().Single()
                : typeof (TResult);

            var sql = new SqlQuery(expression);

            var result = _executeQuery(sql, itemType);
            if (isCollection)
            {
                return (TResult) result;
            }
            return sql.AllowDefault
                ? result.OfType<TResult>().SingleOrDefault()
                : result.OfType<TResult>().Single();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public int ExecuteDelete(Expression expression)
        {
            var delete = new SqlDelete(expression);
            return _executeNonQuery(delete);
        }
    }
}
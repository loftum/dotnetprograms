using System;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Sql.ExtensionMethods;
using DataAccess.Sql.Linq.Statements;
using DataAccess.Sql.Query;
using Microsoft.Practices.ServiceLocation;

namespace DataAccess.UnitTesting.Query
{
    public class TestQueryProvider : IQueryProvider, IDeleteProvider
    {
        public SqlQuery LastQuery { get; private set; }
        public SqlDelete LastDelete { get; private set; }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new DataAccessQueryable<TElement>(this, expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            LastQuery = new SqlQuery(expression);
            if (typeof (TResult).IsCollection())
            {
                var itemType = typeof (TResult).GetGenericArguments().Single();
                return (TResult) Create.ListOf(itemType);
            }
            return default(TResult);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public int ExecuteDelete(Expression expression)
        {
            LastDelete = new SqlDelete(expression);
            return 42;
        }
    }
}
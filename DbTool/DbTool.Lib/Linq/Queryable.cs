using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq
{
    public class Queryable<TSource> : IOrderedQueryable<TSource>
    {
        public Queryable(IQueryProvider provider, IQueryable<TSource> innerSource)
        {
            Provider = provider;
            Expression = Expression.Constant(innerSource);
        }

        public Queryable(IQueryProvider provider, Expression expression)
        {
            Provider = provider;
            Expression = expression;
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<TSource>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Expression Expression
        {
            get;
            private set;
        }

        public IQueryProvider Provider
        {
            get;
            private set;
        }
    }
}
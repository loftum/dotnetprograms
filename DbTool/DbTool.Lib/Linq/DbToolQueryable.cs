using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq
{
    public class DbToolQueryable<TSource> : IOrderedQueryable<TSource>
    {
        public Type ElementType { get { return typeof(TSource); } }
        public Expression Expression { get; private set; }
        public IQueryProvider Provider { get; private set; }

        public DbToolQueryable(IQueryProvider provider)
        {
            Provider = provider;
            Expression = Expression.Constant(this);
        }

        public DbToolQueryable(IQueryProvider provider, Expression expression)
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using StuffLibrary.Common.ExtensionMethods;

namespace StuffLibrary.UnitTesting.Faking
{
    public class FakeQueryOver<TRoot, TSubType> : IQueryOver<TRoot, TSubType>
    {
        private readonly IList<TRoot> _items = new List<TRoot>();

        public FakeQueryOver(params TRoot[] items)
        {
            _items.AddRange(items);
        }

        public FakeQueryOver(IEnumerable<TRoot> items)
        {
            _items.AddRange(items);
        }

        public ICriteria UnderlyingCriteria
        {
            get { throw new NotImplementedException(); }
        }

        public ICriteria RootCriteria
        {
            get { throw new NotImplementedException(); }
        }

        public IList<TRoot> List()
        {
            return _items;
        }

        public IList<U> List<U>()
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TRoot> ToRowCountQuery()
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TRoot> ToRowCountInt64Query()
        {
            throw new NotImplementedException();
        }

        public int RowCount()
        {
            throw new NotImplementedException();
        }

        public long RowCountInt64()
        {
            throw new NotImplementedException();
        }

        public TRoot SingleOrDefault()
        {
            throw new NotImplementedException();
        }

        public U SingleOrDefault<U>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TRoot> Future()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<U> Future<U>()
        {
            throw new NotImplementedException();
        }

        public IFutureValue<TRoot> FutureValue()
        {
            throw new NotImplementedException();
        }

        public IFutureValue<U> FutureValue<U>()
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TRoot> Clone()
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot> ClearOrders()
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot> Skip(int firstResult)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot> Take(int maxResults)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot> Cacheable()
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot> CacheMode(CacheMode cacheMode)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot> CacheRegion(string cacheRegion)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> And(Expression<Func<TSubType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> And(Expression<Func<bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> And(ICriterion expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> AndNot(Expression<Func<TSubType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> AndNot(Expression<Func<bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOverRestrictionBuilder<TRoot, TSubType> AndRestrictionOn(Expression<Func<TSubType, object>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOverRestrictionBuilder<TRoot, TSubType> AndRestrictionOn(Expression<Func<object>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> Where(Expression<Func<TSubType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> Where(Expression<Func<bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> Where(ICriterion expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> WhereNot(Expression<Func<TSubType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> WhereNot(Expression<Func<bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOverRestrictionBuilder<TRoot, TSubType> WhereRestrictionOn(Expression<Func<TSubType, object>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOverRestrictionBuilder<TRoot, TSubType> WhereRestrictionOn(Expression<Func<object>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> Select(params Expression<Func<TRoot, object>>[] projections)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> Select(params IProjection[] projections)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> SelectList(Func<QueryOverProjectionBuilder<TRoot>, QueryOverProjectionBuilder<TRoot>> list)
        {
            throw new NotImplementedException();
        }

        public IQueryOverOrderBuilder<TRoot, TSubType> OrderBy(Expression<Func<TSubType, object>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOverOrderBuilder<TRoot, TSubType> OrderBy(Expression<Func<object>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOverOrderBuilder<TRoot, TSubType> OrderBy(IProjection projection)
        {
            throw new NotImplementedException();
        }

        public IQueryOverOrderBuilder<TRoot, TSubType> OrderByAlias(Expression<Func<object>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOverOrderBuilder<TRoot, TSubType> ThenBy(Expression<Func<TSubType, object>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOverOrderBuilder<TRoot, TSubType> ThenBy(Expression<Func<object>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOverOrderBuilder<TRoot, TSubType> ThenBy(IProjection projection)
        {
            throw new NotImplementedException();
        }

        public IQueryOverOrderBuilder<TRoot, TSubType> ThenByAlias(Expression<Func<object>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> TransformUsing(IResultTransformer resultTransformer)
        {
            throw new NotImplementedException();
        }

        public IQueryOverFetchBuilder<TRoot, TSubType> Fetch(Expression<Func<TRoot, object>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOverLockBuilder<TRoot, TSubType> Lock()
        {
            throw new NotImplementedException();
        }

        public IQueryOverLockBuilder<TRoot, TSubType> Lock(Expression<Func<object>> alias)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, U>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<U>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, U>> path, Expression<Func<U>> alias)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<U>> path, Expression<Func<U>> alias)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, U>> path, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<U>> path, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, U>> path, Expression<Func<U>> alias, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<U>> path, Expression<Func<U>> alias, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, IEnumerable<U>>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<IEnumerable<U>>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, IEnumerable<U>>> path, Expression<Func<U>> alias)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<IEnumerable<U>>> path, Expression<Func<U>> alias)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, IEnumerable<U>>> path, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<IEnumerable<U>>> path, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, IEnumerable<U>>> path, Expression<Func<U>> alias, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<IEnumerable<U>>> path, Expression<Func<U>> alias, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> JoinAlias(Expression<Func<TSubType, object>> path, Expression<Func<object>> alias)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> JoinAlias(Expression<Func<object>> path, Expression<Func<object>> alias)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> JoinAlias(Expression<Func<TSubType, object>> path, Expression<Func<object>> alias, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOver<TRoot, TSubType> JoinAlias(Expression<Func<object>> path, Expression<Func<object>> alias, JoinType joinType)
        {
            throw new NotImplementedException();
        }

        public IQueryOverSubqueryBuilder<TRoot, TSubType> WithSubquery
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryOverJoinBuilder<TRoot, TSubType> Inner
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryOverJoinBuilder<TRoot, TSubType> Left
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryOverJoinBuilder<TRoot, TSubType> Right
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryOverJoinBuilder<TRoot, TSubType> Full
        {
            get { throw new NotImplementedException(); }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq
{
    public class DbToolQueryProvider : IQueryProvider
    {
        private readonly IQueryableToSqlTranslator _translator;
        private readonly IQueryExecutor _executor;
        private readonly DbConnection _dbConnection;

        public DbToolQueryProvider(
            IQueryableToSqlTranslator translator,
            IQueryExecutor executor,
            DbConnection dbConnection)
        {
            _translator = translator;
            _executor = executor;
            _dbConnection = dbConnection;
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

            //IEnumerable queryResult;

            var sql = _translator.Translate(queryable);

            using (var command = _dbConnection.CreateCommand())
            {
                command.CommandText = sql.CommandText;
                foreach (var parameter in sql.Parameters)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Name, parameter.Value));
                }
                return (TResult) _executor.Execute(command);
//                return isCollection
//                           ? (IEnumerable<TResult>) result
//                           : (TResult) result;

//                queryResult = _executor(
//                    itemType,
//                    command.CommandText,
//                    command.Parameters.OfType<DbParameter>()
//                                      .Select(parameter => parameter.Value)
//                                      .ToArray());
            }

//            return isCollection
//                ? (TResult)queryResult
//                : queryResult.OfType<TResult>()
//                             .SingleOrDefault();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
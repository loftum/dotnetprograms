using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DotNetPrograms.Common.Meta;

namespace DbTool.Lib.Linq
{
    public class DbToolQueryProvider : IQueryProvider
    {
        private readonly IQueryableToSqlTranslator _translator;
        private readonly DbConnection _dbConnection;

        public DbToolQueryProvider(
            IQueryableToSqlTranslator translator,
            DbConnection dbConnection)
        {
            _translator = translator;
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

            var sql = _translator.Translate(queryable);

            using (var command = _dbConnection.CreateCommand())
            {
                command.CommandText = sql.CommandText;
                foreach (var parameter in sql.Parameters.Values)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Name, parameter.Value));
                }
                _dbConnection.Open();
                try
                {
                    var result = DoExecute(command, itemType);
                    return isCollection ? (TResult) result : result.OfType<TResult>().SingleOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Could not execute SQL: {0}", command.CommandText), ex);
                }
                finally
                {
                    _dbConnection.Close();
                }
            }
        }

        private static IEnumerable DoExecute(DbCommand command, Type type)
        {
            var items = (IList) new TypeMeta(typeof (List<>).MakeGenericType(type)).NewUp();
            using (var reader = command.ExecuteReader())
            {
                var converter = new RowConverter(type);
                foreach (IDataRecord row in reader)
                {
                    items.Add(converter.Convert(row));
                }
            }
            return items;
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DotNetPrograms.Common.Meta;

namespace MissingLinq.Sql
{
    public class MissingLinqQueryProvider : IQueryProvider
    {
        private readonly IQueryableToSqlTranslator _translator;
        private readonly IDbConnection _dbConnection;

        public MissingLinqQueryProvider(
            IQueryableToSqlTranslator translator,
            IDbConnection dbConnection)
        {
            _translator = translator;
            _dbConnection = dbConnection;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new MissingLinqQueryable<TElement>(this, expression);
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
            var queryable = (IQueryable) Activator.CreateInstance(typeof(MissingLinqQueryable<>).MakeGenericType(itemType), this, expression);

            var sql = _translator.TranslateSelect(queryable);

            using (var command = _dbConnection.CreateCommand())
            {
                command.CommandText = sql.CommandText;
                foreach (var parameter in sql.Parameters.Values)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Name, parameter.Value));
                }
                //_dbConnection.Open();
                try
                {
                    var result = DoExecuteQuery(command, itemType);
                    if (isCollection)
                    {
                        return (TResult) result;
                    }
                    return sql.AllowDefault
                        ? result.OfType<TResult>().SingleOrDefault()
                        : result.OfType<TResult>().Single();
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Could not execute SQL: {0}", command.CommandText), ex);
                }
                finally
                {
                    //_dbConnection.Close();
                }
            }
        }

        private static IEnumerable DoExecuteQuery(IDbCommand command, Type type)
        {
            var items = (IList) new TypeMeta(typeof (List<>).MakeGenericType(type)).NewUp();
            using (var reader = command.ExecuteReader())
            {
                var converter = new RowConverter(type);
                while (reader.Read())
                {
                    items.Add(converter.Convert(reader));
                }
            }
            return items;
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public int ExecuteDelete<TResult>(Expression expression)
        {
            var isCollection = typeof(TResult).IsGenericType && typeof(TResult).GetGenericTypeDefinition() == typeof(IEnumerable<>);
            var itemType = isCollection
                ? typeof(TResult).GetGenericArguments().Single()
                : typeof(TResult);
            var queryable = (IQueryable)Activator.CreateInstance(typeof(MissingLinqQueryable<>).MakeGenericType(itemType), this, expression);
            var sql = _translator.TranslateDelete(queryable);

            using (var command = _dbConnection.CreateCommand())
            {
                command.CommandText = sql.CommandText;
                foreach (var parameter in sql.Parameters.Values)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Name, parameter.Value));
                }
                //_dbConnection.Open();
                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Could not execute SQL: {0}", command.CommandText), ex);
                }
                finally
                {
                    //_dbConnection.Close();
                }
            }
        }
    }
}
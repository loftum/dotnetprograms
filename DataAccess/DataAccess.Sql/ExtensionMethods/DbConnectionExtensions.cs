using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.Sql.Dynamic;
using DataAccess.Sql.Linq.Statements;
using DataAccess.Sql.Mapping;
using DataAccess.Sql.Query;
using DataAccess.Sql.Statements;
using DataAccess.Sql.Stupid;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class DbConnectionExtensions
    {
        public static IQueryable<T> Query<T>(this IDbConnection connection)
        {
            return new DataAccessQueryable<T>(new DataAccessQueryProvider(connection.ExecuteQuery, connection.ExecuteNonQuery));
        }

        public static void OpenIfClosed(this IDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public static int Insert<T>(this IDbConnection connection, T item)
        {
            using (new ConnectionStatePreserver(connection))
            {
                using (var command = connection.CreateCommand())
                {
                    command.MapFrom(InsertStatement.For(item));
                    connection.OpenIfClosed();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }

        public static int InsertInto<T>(this IDbConnection connection, string table, T item)
        {
            using (new ConnectionStatePreserver(connection))
            {
                using (var command = connection.CreateCommand())
                {
                    var statement = InsertStatement.For(item, table);
                    command.MapFrom(statement);
                    connection.OpenIfClosed();
                    var result = command.ExecuteNonQuery();
                    return result;
                }
            }
        }

        public static IEnumerable<dynamic> Query(this IDbConnection connection, string sql, object parameters = null)
        {
            using (new ConnectionStatePreserver(connection))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql.Trim();
                    connection.OpenIfClosed();
                    using (var reader = command.ExecuteReader())
                    {
                        var columns = reader.GetColumns();
                        while (reader.Read())
                        {
                            yield return new DynamicDataRow(columns, reader);
                        }
                    }
                }
            }
        }

        public static int ExecuteNonQuery(this IDbConnection connection, ISqlStatement statement)
        {
            using (new ConnectionStatePreserver(connection))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = statement.CommandText;
                    command.Parameters.AddRange(statement.Parameters);
                    connection.OpenIfClosed();
                    var result = command.ExecuteNonQuery();
                    return result;
                }
            }
        }

        public static IEnumerable ExecuteQuery(this IDbConnection connection, SqlQuery query, Type type)
        {
            using (new ConnectionStatePreserver(connection))
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = query.CommandText;
                        command.Parameters.AddRange(query.Parameters);
                        var list = Create.ListOf(type);
                        connection.OpenIfClosed();
                        using (var reader = command.ExecuteReader())
                        {
                            var converter = TypeMapper.For(type);
                            while (reader.Read())
                            {
                                list.Add(converter.Convert(reader));
                            }
                        }
                        return list;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Could not execute {0}", command.CommandText), ex);
                    }
                }
            }
        }
    }
}
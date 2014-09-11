using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.Sql.Dynamic;
using DataAccess.Sql.ExtensionMethods;
using DataAccess.Sql.Linq.Statements;
using DataAccess.Sql.Mapping;
using DataAccess.Sql.Query;
using DataAccess.Sql.Statements;

namespace DataAccess.Sql
{
    public class SqlDatabase
    {
        private readonly string _connectionString;

        public SqlDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert<T>(T item)
        {
            using (var connection = GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.MapFrom(InsertStatement.For(item));
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }

        public int InsertInto<T>(string table, T item)
        {
            using (var connection = GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    var statement = InsertStatement.For(item, table);
                    command.MapFrom(statement);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }

        public IEnumerable<dynamic> Query(string sql, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql.Trim();
                    connection.Open();
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

        public IQueryable<T> Query<T>()
        {
            return new DataAccessQueryable<T>(new DataAccessQueryProvider(ExecuteQuery, ExecuteNonquery));
        }

        private int ExecuteNonquery(ISqlStatement statement)
        {
            using (var connection = GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = statement.CommandText;
                    command.Parameters.AddRange(statement.Parameters);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }

        private IEnumerable ExecuteQuery(SqlQuery query, Type type)
        {
            using (var connection = GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = query.CommandText;
                        command.Parameters.AddRange(query.Parameters);
                        var list = Create.ListOf(type);
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            var converter = TypeMapper.For(type);
                            while (reader.Read())
                            {
                                list.Add(converter.Convert(reader));
                            }
                        }
                        connection.Close();
                        return list;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Could not execute {0}", command.CommandText), ex);
                    }
                }
            }
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.Linq;

namespace DbTool.Lib.Communication.DbCommands.Dynamic
{
    public class SqlDatabase
    {
        private ConnectionData _connectionData;
        public ConnectionData ConnectionData 
        {
            get { return _connectionData; }
            set
            {
                _connectionData = value;
                _sqlQuery.ConnectionData = _connectionData;
            }
        }

        private DbConnection _dbConnection;
        public DbConnection DbConnection
        {
            get { return _dbConnection; }
            set 
            {
                _dbConnection = value;
                _sqlQuery.DbConnection = _dbConnection;
            }
        }

        private readonly DynamicSqlQuery _sqlQuery;

        public SqlDatabase()
        {
            _sqlQuery = new DynamicSqlQuery();
        }

        public IEnumerable<dynamic> Schema(string collection = null)
        {
            return _sqlQuery.Schema(collection);
        }

        public IEnumerable<dynamic> Query(string sql)
        {
            return _sqlQuery.Query(sql);
        }

        public IQueryable<T> Query<T>()
        {
            return new DbToolQueryable<T>(new DbToolQueryProvider(new QueryableToSqlTranslator(), DbConnection));
        }

        public void Insert<T>(T item)
        {
            _sqlQuery.Insert(item);
        }
    }
}
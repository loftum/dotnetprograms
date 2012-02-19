using System.Collections.Generic;
using System.Data.Common;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.Communication.DbCommands.Dynamic
{
    public class DynamicQuery
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

        public DynamicQuery()
        {
            _sqlQuery = new DynamicSqlQuery();
        }

        public IEnumerable<dynamic> Schema(string collection)
        {
            return _sqlQuery.Schema(collection);
        }

        public IEnumerable<dynamic> Query(string sql)
        {
            return _sqlQuery.Query(sql);
        }
    }
}
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DbToolGui.Communication.Commands
{
    public class QueryExecutor : IDbCommandExecutor
    {
        private readonly SqlConnection _sqlConnection;
        private readonly int _maxRows;

        public QueryExecutor(SqlConnection sqlConnection, int maxRows)
        {
            _sqlConnection = sqlConnection;
            _maxRows = maxRows;
        }

        public IDbCommandResult Execute(string query)
        {
            using (var command = _sqlConnection.CreateCommand())
            {
                try
                {
                    command.CommandText = query.Trim();
                    command.Connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        var numberOfRows = 0;
                        var result = CreateResult(reader);
                        while (reader.Read())
                        {
                            var values = new List<object>();
                            for (var ii = 0; ii < reader.FieldCount; ii++)
                            {
                                values.Add(reader.GetValue(ii));
                            }
                            result.AddRow(values);
                            numberOfRows++;
                            if (_maxRows > 0 && numberOfRows > _maxRows)
                            {
                                break;
                            }
                        }
                        return result;
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        private static QueryResult CreateResult(IDataRecord reader)
        {
            var result = new QueryResult();
            for (var ii = 0; ii < reader.FieldCount; ii++)
            {
                var name = reader.GetName(ii);
                var type = reader.GetFieldType(ii);
                result.AddColumn(name, type);
            }
            return result;
        }
    }
}
using System.Data.SqlClient;

namespace DbToolGui.Connections
{
    public class NonQueryExecutor : IDbCommandExecutor
    {
        private readonly SqlConnection _sqlConnection;

        public NonQueryExecutor(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public IDbCommandResult Execute(string statement)
        {
            using (var command = _sqlConnection.CreateCommand())
            {
                try
                {
                    command.CommandText = statement.Trim();
                    command.Connection.Open();
                    var affectedRows = command.ExecuteNonQuery();
                    return new NonQueryResult(affectedRows);
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
    }
}
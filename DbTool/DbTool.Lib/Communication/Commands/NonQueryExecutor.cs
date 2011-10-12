using System.Data.Common;

namespace DbTool.Lib.Communication.Commands
{
    public class NonQueryExecutor : IDbCommandExecutor
    {
        private readonly DbConnection _dbConnection;

        public NonQueryExecutor(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IDbCommandResult Execute(string statement)
        {
            using (var command = _dbConnection.CreateCommand())
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
using System.Data;
using System.Data.Common;

namespace DbToolGui.Communication.Commands
{
    public class SchemaExecutor : IDbCommandExecutor
    {
        private readonly DbConnection _dbConnection;

        public SchemaExecutor(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IDbCommandResult Execute(string command)
        {
            try
            {
                _dbConnection.Open();
                var schema = GetSchemaFrom(command);
                var result = new QueryResult();

                foreach (DataColumn column in schema.Columns)
                {
                    result.AddColumn(column.ColumnName, column.DataType);
                }
                foreach (DataRow row in schema.Rows)
                {
                    result.AddRow(row.ItemArray);
                }
                return result;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        private DataTable GetSchemaFrom(string command)
        {
            var query = new SchemaQuery(command);
            return query.HasNoCollectionName
                ? _dbConnection.GetSchema()
                : _dbConnection.GetSchema(query.CollectionName, query.RestrictionValues);
        }
    }
}
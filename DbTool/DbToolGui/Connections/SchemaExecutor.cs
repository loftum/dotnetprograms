using System.Data;
using System.Data.SqlClient;

namespace DbToolGui.Connections
{
    public class SchemaExecutor : IDbCommandExecutor
    {
        private readonly SqlConnection _sqlConnection;

        public SchemaExecutor(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public IDbCommandResult Execute(string command)
        {
            try
            {
                _sqlConnection.Open();
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
                _sqlConnection.Close();
            }   
        }

        private DataTable GetSchemaFrom(string command)
        {
            var query = new SchemaQuery(command);
            return query.HasNoCollectionName
                ? _sqlConnection.GetSchema()
                : _sqlConnection.GetSchema(query.CollectionName, query.RestrictionValues);
        }
    }
}
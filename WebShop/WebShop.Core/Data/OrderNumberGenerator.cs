using System.Data.SqlClient;
using WebShop.Common.Configuration;

namespace WebShop.Core.Data
{
    public class OrderNumberGenerator : IOrderNumberGenerator
    {
        private readonly IConfigSettings _settings;

        public OrderNumberGenerator(IConfigSettings settings)
        {
            _settings = settings;
        }

        public long GetNextOrderNumber()
        {
            using (var connection = new SqlConnection(_settings.OrderDbConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetNextOrderNumber";
                    connection.Open();
                    var value = (long) command.ExecuteScalar();
                    connection.Close();
                    return value;
                }
            }
        }
    }
}
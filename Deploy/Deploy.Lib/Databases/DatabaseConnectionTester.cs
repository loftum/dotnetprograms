using System;
using System.Data.SqlClient;

namespace Deploy.Lib.Databases
{
    public class DatabaseConnectionTester : IDatabaseConnectionTester
    {
        public void TestConnection(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new CouldNotConnectException(e.Message, e);
            }
        }
    }
}
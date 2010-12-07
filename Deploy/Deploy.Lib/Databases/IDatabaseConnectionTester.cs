namespace Deploy.Lib.Databases
{
    public interface IDatabaseConnectionTester
    {
        void TestConnection(string connectionString);
    }
}
namespace StuffLibrary.Common.Configuration
{
    public interface IStuffLibraryConfig
    {
        string Databaseprovider { get; }
        string ConnectionString { get; }
    }
}
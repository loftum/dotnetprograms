namespace WebShop.Common.Configuration
{
    public interface IConfigSettings
    {
        string MasterDataConnectionString { get; }
        string OrderDbConnectionString { get; }
        bool EnableNhDiagnostics { get; }
        bool ShowNhSql { get; }
    }
}
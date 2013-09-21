namespace WebShop.Common.Configuration
{
    public interface IConfigSettings
    {
        string MasterDataConnectionString { get; }
        bool EnableNhDiagnostics { get; }
        bool ShowNhSql { get; }
    }
}
namespace VisualFarmStudio.Common.Configuration
{
    public interface IVFSConfig
    {
        string Behave { get; set; }
        string ConnectionString { get; }
    }
}
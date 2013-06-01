namespace DotNetPrograms.Common.Mapping.Internal
{
    public interface IPropertyMap
    {
        string FullName { get; }
        void Map(object source, object target); 
    }
}
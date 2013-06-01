namespace DotNetPrograms.Common.Mapping.Internal
{
    public interface ITypeMap
    {
        void Map(object source, object target);
        object Map(object source);
    }
}
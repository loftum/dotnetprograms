using AutoMapper;

namespace BasicManifest.Lib.ExtensionMethods
{
    public static class AutoMapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object item)
        {
            return Mapper.Map<TDestination>(item);
        }
    }
}
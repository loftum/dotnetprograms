namespace StuffLibrary.Domain.ExtensionMethods
{
    public static class DomainObjectExtensions
    {
         public static bool IsNew(this DomainObject domainObject)
         {
             return domainObject.Id == 0;
         }
    }
}
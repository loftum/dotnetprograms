namespace DotNetPrograms.Common.Validation
{
    public interface IPropertyValidator<in TProperty>
    {
        void Validate(TProperty property);
    }
}
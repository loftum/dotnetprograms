namespace DeployWizard.Lib.Validation
{
    public interface IValidator<in T>
    {
        bool IsValid(T item);
        void Validate(T item);
    }
}
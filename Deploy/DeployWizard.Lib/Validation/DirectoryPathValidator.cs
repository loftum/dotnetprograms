using System.IO;

namespace DeployWizard.Lib.Validation
{
    public class DirectoryPathValidator : IValidator<string>
    {
        public bool IsValid(string path)
        {
            return Directory.Exists(path);
        }

        public void Validate(string path)
        {
            if (File.Exists(path))
            {
                throw new ValidationException(path + " must be a directory");
            }
            if (!IsValid(path))
            {
                throw new ValidationException(path + " does not exist");
            }
        }
    }
}
using System.IO;

namespace DeployWizard.Lib.Validation
{
    public class FilePathValidator : IValidator<string>
    {
        public bool IsValid(string path)
        {
            return File.Exists(path);
        }

        public void Validate(string path)
        {
            if (Directory.Exists(path))
            {
                throw new ValidationException(path + " must be a file.");
            }
            if (!IsValid(path))
            {
                throw new ValidationException(path + " does not exist.");
            }
        }
    }
}
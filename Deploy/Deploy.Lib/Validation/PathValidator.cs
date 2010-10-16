using System.IO;

namespace Deploy.Lib.Validation
{
    public class PathValidator
    {
        public void VerifyDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new ValidationException("Directory " + path + " does not exist");
            }
        }

        public void VerifyFileExists(string path)
        {
            if (!File.Exists(path))
            {
                throw new ValidationException("File " + path + " does not exist");
            }
        }
    }
}
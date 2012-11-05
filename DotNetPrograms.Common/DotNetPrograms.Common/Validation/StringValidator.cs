using DotNetPrograms.Common.Exceptions;

namespace DotNetPrograms.Common.Validation
{
    public class StringValidator
    {
        private string _includeString = string.Empty;

        public StringValidator Contains(string value)
        {
            _includeString = value;
            return this;
        }

        public void Validate(string value)
        {
            if (!value.Contains(_includeString))
            {
                throw new UserException("Må innholde: {0}", _includeString);
            }
        }
    }
}
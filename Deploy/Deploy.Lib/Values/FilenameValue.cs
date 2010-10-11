using System.Text.RegularExpressions;

namespace Deploy.Lib.Values
{
    public class FilenameValue : Value<string>
    {
        public FilenameValue(string value) : base(value)
        {
        }

        public bool HasExtension()
        {
            var regex = new Regex(@".*\.{1}.+");
            return regex.IsMatch(TheValue);
        }

        public bool IsExtensionless()
        {
            return !HasExtension();
        }
    }
}

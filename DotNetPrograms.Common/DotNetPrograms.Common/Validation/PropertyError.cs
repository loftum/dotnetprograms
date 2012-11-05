namespace DotNetPrograms.Common.Validation
{
    public class PropertyError
    {
        public PropertyError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", PropertyName, ErrorMessage);
        }
    }
}
namespace DbTool.Lib.CSharp
{
    public class CSharpResult
    {
        public bool HasMessage { get { return !string.IsNullOrEmpty(Message); } }

        public bool ResultSet { get; private set; }
        public object Result { get; private set; }
        public string Message { get; private set; }

        public CSharpResult(bool resultSet, object result, string message)
        {
            Message = message;
            Result = result;
            ResultSet = resultSet;
        }
    }
}
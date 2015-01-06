using MongoTool.Core.ExtensionMethods;

namespace MongoTool.Core.CSharp
{
    public class CSharpResult
    {
        public string Output { get; private set; }
        public object Result { get; private set; }
        public bool ResultSet { get; private set; }
        public string Report { get; private set; }

        public CSharpResult(string output, object result, bool resultSet, string report)
        {
            Report = report;
            Output = output;
            Result = result;
            ResultSet = resultSet;
        }

        public override string ToString()
        {
            var value = Output ?? Result ?? Report;
            return value == null ? "null" : value.ToJson(true, true);
        }
    }
}
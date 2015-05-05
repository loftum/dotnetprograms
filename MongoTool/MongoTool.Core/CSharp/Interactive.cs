using MongoTool.Core.Data;

namespace MongoTool.Core.CSharp
{
    public class Interactive
    {
        public static MongoDb Db { get; private set; }
        public static CSharpEvaluator Evaluator { get; private set; }

        static Interactive()
        {
            Evaluator = new CSharpEvaluator();
            Db = new MongoDb();
        }
    }
}
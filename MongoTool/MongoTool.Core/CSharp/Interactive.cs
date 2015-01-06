using MongoTool.Core.Data;

namespace MongoTool.Core.CSharp
{
    public class Interactive
    {
        public static MongoDb Db { get; private set; }

        static Interactive()
        {
            Db = new MongoDb();
        }
    }
}
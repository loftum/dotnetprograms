using StructureMap;

namespace MongoTool.Ioc
{
    public class Di
    {
        public static IContainer Container { get; private set; }

        static Di()
        {
            Container = new Container(new MongoToolRegistry());
        }
    }
}
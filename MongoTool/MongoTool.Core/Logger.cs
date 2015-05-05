using System.IO;

namespace MongoTool.Core
{
    public class Logger
    {
        private const string Filename = "MongoTool.log";

        static Logger()
        {
            using (File.Create(Filename))
            {
            }
        }

        public static void Log(string text)
        {
            File.AppendAllLines(Filename, new []{text});
        }
    }
}
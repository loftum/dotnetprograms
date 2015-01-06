namespace MongoTool.Core.Configuration
{
    public class DbConfig
    {
        public string DbConnectionString { get { return "mongodb://gaus:11gris@localhost:27017/brukerbasedev"; } }
        public string DatabaseName { get { return "brukerbasedev"; } }
    }
}
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoTool.Core.Data
{
    public class MongoDb
    {
        private MongoDatabase _database;
        private MongoServer _server;

        public void Connect(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _server = client.GetServer();
        }

        public void Use(string database)
        {
            _database = _server.GetDatabase(database);
        }

        public WriteConcernResult Insert<T>(T item)
        {
            return GetCollection<T>().Insert(item);
        }

        public WriteConcernResult Delete<T>(string id)
        {
            return GetCollection<T>().Remove(Query.EQ("_id", id));
        }

        public WriteConcernResult Delete<T>(T item)
        {
            return GetCollection<T>().Remove(Query.EQ("_id", Cleverly.GetIdFrom(item).ToBson()));
        }

        public T Get<T>(string id)
        {
            return GetCollection<T>().FindOneByIdAs<T>(id);
        }

        public IQueryable<T> GetAll<T>()
        {
            return GetAll<T>(typeof (T).Name);
        }

        public IQueryable<T> GetAll<T>(string collectionName)
        {
            return GetCollection<T>(collectionName).AsQueryable();
        }

        private MongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        private MongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
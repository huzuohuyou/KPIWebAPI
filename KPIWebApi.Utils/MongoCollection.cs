using MongoDB.Driver;
using MongoDB.Bson;

namespace FrameWork
{
    public class MongoCollection<T> where T : class, new()
    {
        public static string conn = "mongodb://localhost:27017";
        static MongoClient client;
        public static string dbName = "test";
        private static IMongoCollection<T> collection;
        private MongoCollection()
        {
           
        }
        public static IMongoCollection<T> GetInstance()
        {
            if (collection==null)
            {
                client = new MongoClient(conn);
                var db = client.GetDatabase(dbName);
                collection = db.GetCollection<T>(typeof(T).ToString().Split('+')[1]);
            }
            return collection;
        }

    }
}
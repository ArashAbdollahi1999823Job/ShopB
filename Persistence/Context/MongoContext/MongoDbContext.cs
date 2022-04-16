using Application.Interfaces;
using MongoDB.Driver;

namespace Persistence.Context.MongoContext
{
    public class MongoDbContext<T>:IMongoDbContext<T>
    {

        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoDbContext()
        {
            var client = new MongoClient();
            _db = client.GetDatabase("VisitorDb");
            _mongoCollection = _db.GetCollection<T>(typeof(T).Name);


        }

        public IMongoCollection<T> GetCollection()
        {
            return _mongoCollection;
        }
    }
}

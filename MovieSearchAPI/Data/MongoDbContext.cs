using MongoDB.Driver;
using MovieSearchAPI.Models;

namespace MovieSearchAPI.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionURI);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}

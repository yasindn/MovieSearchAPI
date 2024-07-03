using MongoDB.Driver;

namespace MovieSearchAPI.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}

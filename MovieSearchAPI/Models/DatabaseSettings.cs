namespace MovieSearchAPI.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ConnectionURI { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}

using MongoDB.Driver;
using WebApplication4.Interface;
using WebApplication4.Models;

namespace WebApplication4.servises
{
    public class MongoService : IMongoService
    {
        public readonly IMongoDatabase _database;
        public MongoService(IConfiguration config)
        {

            var connectionString = config.GetConnectionString("DefaultConnection");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(config["DatabaseName"]);
        }

        //method
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}

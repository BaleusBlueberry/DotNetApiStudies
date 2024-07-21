using MongoDB.Driver;
using WebApplication4.Models;

namespace WebApplication4.Interface
{
    public interface IMongoService
    {
        public IMongoCollection<T> GetCollection<T>(string name);
    }
}

using Humanizer;
using MongoDB.Driver;
using WebApplication4.Interface;

namespace WebApplication4.Repository
{
    public class Repository<T>(IMongoService mongo) : IRepository<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> _collection = mongo.GetCollection<T>(typeof(T).Name.Pluralize());
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var cursor = await _collection.FindAsync(item => true);
            return await cursor.ToListAsync();
        }

        public virtual async Task<T> AddAsync(T item)
        {
            await _collection.InsertOneAsync(item);
            return item;
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<T?> EditAsync(T item, string id)
        {
            var result = await _collection.ReplaceOneAsync(m => m.Id == id, item);

            if (result.ModifiedCount == 0)
            {
                return default(T);
            }

            return item;
        }

        public virtual async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(item => item.Id == id);
        }
    }
}

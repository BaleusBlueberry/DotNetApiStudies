namespace WebApplication4.Repository
{
    public interface IRepository<T> where T : IEntity //Add type narrowing
    {
            Task<IEnumerable<T>> GetAll();
            Task<T> AddAsync(T item);
            Task<T?> GetByIdAsync(string id);
            Task<T?> EditAsync(T item, string id);
            Task DeleteAsync(string id);
    }
}

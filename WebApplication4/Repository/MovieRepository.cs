using WebApplication4.Interface;
using WebApplication4.Models;

namespace WebApplication4.Repository
{
    public class MovieRepository(IMongoService monogo) : Repository<Movie>(monogo)
    {
    }
}

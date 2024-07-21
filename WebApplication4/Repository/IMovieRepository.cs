using WebApplication4.Models;

namespace WebApplication4.Repository
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> AddMovieAsync(Movie movie);
        Task<Movie?> GetMovieByIdAsync(string id);
        Task<Movie?> EditMovieAsync(Movie movie, string id);
    }
}

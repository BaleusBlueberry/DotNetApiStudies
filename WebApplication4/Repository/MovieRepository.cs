using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplication4.Interface;
using WebApplication4.Models;

namespace WebApplication4.Repository
{
    public class MovieRepository(IMongoService mongo) : IMovieRepository
    {
        private readonly IMongoCollection<Movie> _movies = mongo.GetCollection<Movie>("movies");
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var cursor = _movies.Find(_ => true);

            var movies = await cursor.ToListAsync();
            
            return movies;
        }

        public async Task<Movie> AddMovieAsync([FromBody]Movie movie)
        {
            await _movies.InsertOneAsync(movie);

            return movie;
        }

        public async Task<Movie?> GetMovieByIdAsync(string id)
        {
            var results = _movies.Find(m => m.Id == id);
            var movie = await results.FirstOrDefaultAsync();
            return movie;
        }

        public async Task<Movie?> EditMovieAsync(Movie movie, string id)
        {
            var result = await _movies.ReplaceOneAsync(m => m.Id == id, movie);

            if (result.ModifiedCount == 0)
            {
                return null;
            }

            return movie;
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Repository;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(IMovieRepository repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await repo.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var movie = await repo.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return BadRequest();
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> PostMovie([FromBody]Movie movie)
        {
            var result = await repo.AddMovieAsync(movie);

            // status 201 = Created 
            // filter
            return CreatedAtAction(nameof(Get), new { Id=movie.Id! }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(string id, [FromBody]Movie movie)
        {
            var result = await repo.EditMovieAsync(movie, id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

    }
}

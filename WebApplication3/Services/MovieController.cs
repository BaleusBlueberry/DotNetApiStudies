using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplication3.Controllers;
using WebApplication3.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public IMongoCollection<Movie> movie { get; set; }

        // GET: api/<MovieController>
        public MovieController(MongoService s)
        {
            movie = s.GetCollection<Movie>("Movies");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(movie.Find(m => true).ToList());
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var m = movie.Find(mov => mov.Id == id).FirstOrDefault();
            if (m == null) return NotFound();
            return Ok(m);
        }

        // POST api/<MovieController>
        [HttpPost]
        public IActionResult PostMovie([FromBody] Movie _movie)
        {
            var m = movie.Find(mov => mov.Description == _movie.Description && mov.Title == _movie.Title).FirstOrDefault();
            if (m == null)
            {
                movie.InsertOne(_movie);
                return Ok(_movie);
            }

            return BadRequest("movie already exists");
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
            var _movie = movie.Find(m => m.Id == id);
        } // here ********** continue here

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

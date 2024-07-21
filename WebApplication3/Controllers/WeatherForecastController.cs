using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{

    public IMongoCollection<Person> people { get; set; }


    public PeopleController(MongoService s)
    {
        people = s.GetCollection<Person>("People");

    }

    [HttpPost]
    public IActionResult PostPerson([FromBody] Person person)
    {
        people.InsertOne(person);
        return Ok(person);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(people.Find(p => true).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var person = people.Find(p => p.Id == id).FirstOrDefault();
        if (person == null) return NotFound();
        return Ok(person);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var result = people.DeleteOne(p => p.Id == id);
        if (result.DeletedCount == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Put(string id, Person p)
    {
        var result = people.ReplaceOne(Person => Person.Id == id, p);
        if (result.ModifiedCount == 0)
        {
            return NotFound();
        }
        return Ok(p);
    }

    
}
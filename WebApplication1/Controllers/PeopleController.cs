using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        static List<Person> people =
        [
            new Person("John", 30),
            new Person("Jane", 25),
            new Person("Joe", 40)
        ];

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return people;
        }

        [HttpGet("{id}")]
        public Person? Get(Guid id)
        {
            return people.Find(p => p.Id == id);
            //other ways 
            //return people.Where(p => p.Id == id).FirstOrDefault();
            //return people.FirstOrDefault(p => p.Id == id);

        }

        [HttpGet("byName/{name}")]
        public IEnumerable<Person> Get(string name)
        {
            return people.Where(p => p.Name.Contains(name));
            //other ways 
            //return people.Where(p => p.Id == id).FirstOrDefault();
            //return people.FirstOrDefault(p => p.Id == id);

        }

        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            var person = people.Find(p => p.Id == id);

            if (person != null)
            {
                people.Remove(person);
                return Ok(person);
            }
            else
            {
                return NotFound($"Person {id} Dose not Exists");
            }

        }

        [HttpPost]
        public Person Post(Person p)
        {
            people.Add(p);
            p.Id = Guid.NewGuid();
            return p;
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Person p)
        {
            var person = people.Find(pp => pp.Id == id);

            if (person != null)
            {
                person.Name = p.Name;
                person.Age = p.Age;
                return Ok(person);
            }
            else
            {
                return NotFound($"Person {id} Dose not Exists");
            }
        }

    }
}


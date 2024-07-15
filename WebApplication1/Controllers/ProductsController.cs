using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        static List<Product> products =
        [
            new Product("appes", 23, 23),
            new Product("bannans", 15, 5),
            new Product("blueberries", 32, 2)
        ];
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(products);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = products.Find(p => p.Id == id);

            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            products.Add(product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Product product)
        {
            var _product = products.Find(pp => pp.Id == id);

            if (_product != null)
            {
                //Console.WriteLine("reached this point");
                _product.Name = product.Name;
                _product.Price = product.Price;
                _product.Quantity = product.Quantity;
                return Ok(_product);
            }
            else
            {
                return NotFound($"product {id} Dose not Exists");
            }
        }
        [HttpPut("buy/{id}")]
        public IActionResult Put(Guid id, [FromBody] int amount)
        {
            var _product = products.Find(pp => pp.Id == id);

            if (_product != null)
            {
                if (_product.Quantity >= amount)
                {
                    _product.Quantity = _product.Quantity - amount;
                    return Ok(_product.Quantity);
                }
                else
                {
                    return BadRequest("Product amount is too big / not enough products");
                }
            }
            else
            {
                return NotFound($"product {id} Dose not Exists");
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var _product = products.Find(pp => pp.Id == id);

            if (_product != null)
            {
                products.Remove(_product);
                return Ok(_product);
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}

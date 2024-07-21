using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private static readonly string connectionString = "mongodb+srv://ssaasshh29:h4Gfz82Bk96Fro2Y@cluster0.dlz0x99.mongodb.net/";
    private static readonly MongoClient client = new MongoClient(connectionString);
    private static readonly IMongoDatabase database = client.GetDatabase("TestShop");
    private static readonly IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Shopitems");

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Attempting to retrieve documents from MongoDB");

        try
        {
            var documents = await collection.Find(new BsonDocument()).ToListAsync();
            _logger.LogInformation($"Retrieved {documents.Count} documents from MongoDB");

            var products = new List<Product>();

            foreach (var doc in documents)
            {
                _logger.LogInformation($"Processing document: {doc}");

                var product = new Product
                {
                    Id = doc["_id"].AsObjectId.ToString(),
                    Name = doc["name"].AsString,
                    Price = doc["price"].AsDouble,
                    Stock = doc["stock"].AsInt32
                };
                products.Add(product);
            }

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving documents: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    /* // GET api/<ProductsController>/5
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
     }*/
}
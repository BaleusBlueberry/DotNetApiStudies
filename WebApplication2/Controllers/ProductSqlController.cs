using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using WebApplication2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSqlController : ControllerBase
    {
        static string connectionString = "Server=BALEUS-BLUEBERR\\MSSQLSERVERFIRST;Database=MyShop;Trusted_Connection=True";
        /*SqlConnection connection = new SqlConnection(connectionString);*/

        // GET: api/<ProductSqlController>
        [HttpGet]
        public IActionResult Get()
        {
            List<ProductSQL> products = new List<ProductSQL>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT [ID], [Name], [Price], [Description], [CategoryID], [Quantity] FROM [Products]";
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductSQL product = new ProductSQL(
                            reader.GetInt32(reader.GetOrdinal("ID")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetDouble(reader.GetOrdinal("Price")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            reader.GetInt32(reader.GetOrdinal("Quantity"))
                        );
                        products.Add(product);
                    }
                }
            }

            return Ok(products);
        }

        // GET api/<ProductSqlController>/5
        [HttpGet("ById/{id}")]
        public IActionResult Get(int id)
        {
            ProductSQL product = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT [ID], [Name], [Price], [Description], [CategoryID], [Quantity] FROM [Products] WHERE [ID] = @ID";
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        product = new ProductSQL(
                            reader.GetInt32(reader.GetOrdinal("ID")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetDouble(reader.GetOrdinal("Price")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            reader.GetInt32(reader.GetOrdinal("Quantity"))
                        );
                    }
                }
            }

            if (product == null)
            {
                return NotFound(id + " was not found");
            }

            return Ok(product);
        }

        [HttpGet("ByName/{name}")]
        public IActionResult Get(string name)
        {
            List<ProductSQL> products = new List<ProductSQL>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT [ID], [Name], [Price], [Description], [CategoryID], [Quantity] FROM [Products] WHERE [Name] LIKE @name";
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductSQL product = new ProductSQL(
                            reader.GetInt32(reader.GetOrdinal("ID")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetDouble(reader.GetOrdinal("Price")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            reader.GetInt32(reader.GetOrdinal("Quantity"))
                        );
                        products.Add(product);
                    }
                }
            }

            if (products.Count == 0)
            {
                return NotFound(name + " was not found");
            }

            return Ok(products);
        }

        // POST api/<ProductSqlController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductSQL newProduct)
        {
            if (await FindIfProductExistsAsync(newProduct.Name))
            {
                return Conflict("Product already exists.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [Products] ([Name], [Price], [Description], [CategoryID], [Quantity]) VALUES (@Name, @Price, @Description, @CategoryID, @Quantity)";
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", newProduct.Name);
                    cmd.Parameters.AddWithValue("@Price", newProduct.Price);
                    cmd.Parameters.AddWithValue("@Description", newProduct.Description);
                    cmd.Parameters.AddWithValue("@CategoryID", newProduct.CategoryID);
                    cmd.Parameters.AddWithValue("@Quantity", newProduct.Quantity);
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return CreatedAtAction(nameof(Get), new { id = newProduct.ID }, newProduct);
        }


        // PUT api/<ProductSqlController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductSqlController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private async Task<bool> FindIfProductExistsAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT 1 FROM [Products] WHERE [Name] = @Name";
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
    }
}

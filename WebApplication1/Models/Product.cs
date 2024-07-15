namespace WebApplication1.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { set; get; }
        public int Quantity { get; set; }

        // alt enter ==> generate constractor

        public Product(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Id = Guid.NewGuid();
        }
    }
}

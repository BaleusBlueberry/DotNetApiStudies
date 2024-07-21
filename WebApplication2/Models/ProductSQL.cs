namespace WebApplication2.Models;

public class ProductSQL
{
    public int? ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public int CategoryID { get; set; }
    public int Quantity { get; set; }

    // alt enter ==> generate constractor
    public ProductSQL(int id, string name, double price, string description, int categoryID, int quantity)
    {
        ID = id;
        Name = name;
        Price = price;
        Description = description;
        CategoryID = categoryID;
        Quantity = quantity;
    }
    public ProductSQL(string name, double price, string description, int categoryID, int quantity)
    {
        Name = name;
        Price = price;
        Description = description;
        CategoryID = categoryID;
        Quantity = quantity;
    }
}


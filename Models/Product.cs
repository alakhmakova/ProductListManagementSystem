namespace ProductListManagementSystem.Models;

//data for a single product
public class Product
{
    public Product(string category, string name, decimal price)
    {
        Category = category;
        Name = name;
        Price = price;
    }

    public string Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

}

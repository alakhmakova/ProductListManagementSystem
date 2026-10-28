namespace ProductListManagementSystem.Models;

//data for a single product
public class Product
{
    public Product(string category, string name, decimal price)
    {
        Catigory = category;
        Name = name;
        Price = price;
    }

    public string Catigory { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

}

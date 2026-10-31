namespace ProductListManagementSystem.Models;

// What is saved to the file: the products and the next free id
public class ProductData
{
    public int NextId { get; set; } = 1;
    public List<Product> Products { get; set; } = new();
}

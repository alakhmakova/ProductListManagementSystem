using ProductListManagementSystem.Exceptions;
namespace ProductListManagementSystem.Models;

//data for a single product
public class Product
{
    public Product(string category, string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ValidationException("Product category can't be empty");
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException("Product name can't be empty");
        }
        if (!IsValidPrice(price))
        {
            throw new ValidationException("Price must be a number greater than 0 with at most 2 decimals");
        }
        Category = category;
        Name = name;
        Price = price;

    }

    public string Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public static bool IsValidPrice(decimal price)
    {
        return price > 0 && (Math.Round(price, 2) == price);
    }

}

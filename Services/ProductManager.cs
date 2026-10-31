using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;

namespace ProductListManagementSystem.Services;

// Stores the products in a List and does all work on it
public class ProductManager
{
    private readonly List<Product> _products = new();
    // True if the list is not empty (computed on every read)
    public bool HasProducts => _products.Count > 0;
    // Add a product to the list
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    // Search by part of name or category, case-insensitive
    public List<Product> SearchProduct(string search)
    {
        List<Product> found = _products
        .Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || p.Category.Contains(search, StringComparison.OrdinalIgnoreCase))
        .ToList();

        if (found.Count == 0)
        {
            throw new ProductsNotFoundException();
        }
        return found;
    }

    // Products sorted from lowest to highest price
    public List<Product> GetProductsSortedByPrice()
    {

        return _products.OrderBy(p => p.Price).ToList();


    }

    // Total price of all products, 0 for an empty list
    public decimal CalculateTotal()
    {
        return _products.Sum(p => p.Price);
    }

    // Replace the list with products loaded from the file
    public void LoadProducts(List<Product> products)
    {
        _products.Clear();// readonly forbids replacing the list, but its contents can still change
        _products.AddRange(products);
    }


}

using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;

namespace ProductListManagementSystem.Services;

// Stores the products in a List and does all work on it
public class ProductManager
{
    private readonly List<Product> _products = new();
    // Next id to give out; ids are never reused, even after a delete
    private int _nextId = 1;
    // True if the list is not empty (computed on every read)
    public bool HasProducts => _products.Count > 0;
    // Give the product the next id and add it to the list
    public void AddProduct(Product product)
    {
        product.Id = _nextId;
        _nextId++;
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

    // Products and next id, ready to be saved
    public ProductData GetData()
    {
        return new ProductData { NextId = _nextId, Products = GetProductsSortedByPrice() };
    }

    // Replace the list and the next id with data loaded from the file
    public void LoadData(ProductData data)
    {
        _products.Clear(); // readonly forbids replacing the list, but its contents can still change
        _products.AddRange(data.Products);
        // Never below the biggest existing id + 1, even if the file was edited by hand
        int maxId = _products.Count > 0 ? _products.Max(p => p.Id) : 0;
        _nextId = Math.Max(data.NextId, maxId + 1);
    }


}

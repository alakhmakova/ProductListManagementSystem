using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;

namespace ProductListManagementSystem.Services;

//holds the product list and works with it
public class ProductManager
{
    //Index was outside of the bounds of the Array IndexOutOfRangeException

    private readonly List<Product> _products = new();
    // Computed property: has no stored value, recalculated on every read.
    public bool HasProducts => _products.Count > 0;
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public List<Product> SearchProduct(string search)
    {
        List<Product> found = _products
        .Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || p.Category.Contains(search, StringComparison.OrdinalIgnoreCase)) // Case-insensitive search by partial product name using LINQ 
        .ToList();

        if (found.Count == 0)
        {
            throw new ProductsNotFoundException();
        }
        return found;
    }

    public List<Product> GetProductsSortedByPrice()
    {

        //Sort products from lowest to highest price. 
        // List can be empty
        return _products.OrderBy(p => p.Price).ToList();


    }

    public decimal CalculateTotal()
    {
        //If the list is empty sum = 0
        return _products.Sum(p => p.Price);
    }


}

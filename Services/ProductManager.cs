using ProductListManagementSystem.Models;

namespace ProductListManagementSystem.Services;

//holds the product list and works with it
public class ProductManager
{
    //Index was outside of the bounds of the Array IndexOutOfRangeException

    private readonly List<Product> _products = new();
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public void SearchProduct()

    {
        Console.WriteLine("Results: ");
    }

    public void ListProducts()
    {
        Console.WriteLine("Products: ");
    }
}

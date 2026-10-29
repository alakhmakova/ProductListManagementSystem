using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;
using ProductListManagementSystem.UI;

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
        /*
        A) Search Function
            Allow users to search for products by:
                • Product Name 
                • Category 
        B) Highlight Searched Product
            When a product is found:
                • Highlight it using console colors 
        */
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

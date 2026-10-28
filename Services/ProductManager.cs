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
        Console.WriteLine("Results: ");
    }

    public void ListProducts()
    {
        //empty list + table formatting
        if (_products.Count > 0)
        {
            //Sort products from lowest to highest price
            List<Product> sortedProducts = _products.OrderBy(p => p.Price).ToList();
            InputHelper.ShowInfo("Category".PadRight(20) + "|" + "Product name".PadRight(20) + "|" + "Price");
            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(product.Category.PadRight(20) + "|" + product.Name.PadRight(20) + "|" + product.Price);
            }
        }
        else
        {
            throw new EmptyListException();
        }
    }



}

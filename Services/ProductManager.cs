using System;
using ProductListManagementSystem.Models;

namespace ProductListManagementSystem.Services;

//holds the product list and works with it
public class ProductManager
{
//Index was outside of the bounds of the Array IndexOutOfRangeException
List<Product> products = new List<Product>();
public void AddProduct()
{
System.Console.WriteLine("The product was successfully added.");
}

public void SearchProduct()

{
System.Console.WriteLine("Results: ");
}

public void ListProducts()
{
System.Console.WriteLine("Products: ");
}
}

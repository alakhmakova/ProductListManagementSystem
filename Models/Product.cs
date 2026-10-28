using System;

namespace ProductListManagementSystem.Models;

//data for a single product
public class Product
{
    public Product(string catigory, string name, decimal price)
    {
        Catigory = catigory;
        Name = name;
        Price = price;
    }

public string Catigory { get; set; }
public string Name { get; set; }
public decimal Price { get; set; }

}

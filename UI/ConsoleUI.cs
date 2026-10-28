using System;
using ProductListManagementSystem.Services;

namespace ProductListManagementSystem.UI;

//menu, table output, colors
public class ConsoleUI
{
    ProductManager manager = new ProductManager();
public void Run()
    {
        Console.WriteLine("**** PRODUCT LIST APPLICATION ****");
        while(true){
            Console.WriteLine("Enter a Number");
            Console.WriteLine("1-Add a Product");
            Console.WriteLine("2-Search a Product");
            Console.WriteLine("3-List Products");
            Console.WriteLine("0-Quit");

            Console.Write("Enter a Number");
            string userInput = Console.ReadLine();
                switch (userInput)
                    {
                        case "1":
                            manager.AddProduct();
                            break;
                        case "2":
                            manager.SearchProduct();
                            break;
                        case "3":
                            manager.ListProducts();
                            break;
                        case "0":
                            Console.WriteLine("Thank you for using this application");
                            break;

                        default:
                            Console.WriteLine("Invalid Selection");
                            //ShowMainMenu();
                            break;
}

}
    }
}

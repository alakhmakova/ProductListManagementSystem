using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;
using ProductListManagementSystem.Services;

namespace ProductListManagementSystem.UI;

//menu, table output, colors
public class ConsoleUI
{
    ProductManager manager = new ProductManager();
    public void Run()
    {
        Console.WriteLine("**** PRODUCT LIST APPLICATION ****");
        bool run = true;
        while (run)
        {
            Console.WriteLine("");
            Console.WriteLine("1-Add a Product");
            Console.WriteLine("2-Search a Product");
            Console.WriteLine("3-List Products");
            Console.WriteLine("0-Quit");


            string menuOption = InputHelper.ReadText("Select Option 0-3: ", "Menu option can't be empty");
            switch (menuOption)
            {
                case "1":
                    InputHelper.ShowInfo("\nTo enter a new product - follow the steps | To quit - enter 'Q'\n");
                    try
                    {
                        while (true)
                        {
                            string category = InputHelper.ReadTextOrCancel("Enter Category: ", "Product Category can't be empty");
                            string name = InputHelper.ReadTextOrCancel("Enter Product name: ", "Product name can't be empty");
                            decimal price = InputHelper.ReadPrice("Enter Price: ", "Price can't be empty", "Product price should be grater then 0");
                            manager.AddProduct(new Product(category, name, price));
                            InputHelper.ShowSuccess("The product was succesfully added!\n ");
                            continue;
                        }
                    }
                    catch (UserCancelledException e)
                    {
                        InputHelper.ShowInfo(e.Message);
                    }
                    break;
                case "2":
                    manager.SearchProduct();
                    break;
                case "3":
                    try { manager.ListProducts(); } catch (EmptyListException e) { InputHelper.ShowInfo(e.Message); }
                    break;
                case "0":
                    InputHelper.ShowSuccess("Thank you for using this application");
                    run = false;
                    break;

                default:
                    InputHelper.ShowError("Invalid selection\n");
                    //ShowMainMenu();
                    break;
            }


        }
    }
    void ExitFromAddProduct(string input)
    {
        if (input.Equals("q"))
        {
            return;
        }
    }

}

using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;
using ProductListManagementSystem.Services;

namespace ProductListManagementSystem.UI;

//menu, table output, colors
public class ConsoleUI
{
    // private: not accessible outside this class
    // readonly: can't be replaced after creation, so the product list is never lost
    // _manager: naming convention for private fields
    private readonly ProductManager _manager = new();
    private const int SeparatorCount = 2; // syblos | in the table
    private const int ColumnPadding = 2; // extra spaces added to each column width
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
                    InputHelper.ShowInfo(new string('-', 63));
                    InputHelper.ShowInfo("To enter a new product - follow the steps | To quit - enter 'Q'");
                    InputHelper.ShowInfo(new string('-', 63));
                    try
                    {
                        while (true)
                        {
                            string category = InputHelper.ReadTextOrCancel("Enter Category: ", "Product Category can't be empty");
                            string name = InputHelper.ReadTextOrCancel("Enter Product name: ", "Product name can't be empty");
                            decimal price = InputHelper.ReadPrice("Enter Price: ", "Price can't be empty", "Price must be a number greater than 0 with at most 2 decimals");
                            _manager.AddProduct(new Product(category, name, price));
                            InputHelper.ShowSuccess("The product was succesfully added!\n ");
                            continue;
                        }
                    }
                    catch (UserCancelledException e)
                    {
                        InputHelper.ShowWarning(e.Message);
                    }
                    break;
                case "2":
                    break;
                case "3":
                    try
                    {
                        var products = _manager.GetProductsSortedByPrice();
                        var (categoryWidth, nameWidth, priceWidth) = GetColumnWidths(products);
                        int tableWidth = categoryWidth + nameWidth + priceWidth + SeparatorCount;
                        InputHelper.ShowInfo(new string('-', tableWidth));
                        InputHelper.ShowInfo("Category".PadRight(categoryWidth) + " " + "Product name".PadRight(nameWidth) + " " + "Price".PadLeft(priceWidth));
                        InputHelper.ShowInfo(new string('-', tableWidth));
                        ListProducts(products, categoryWidth, nameWidth, priceWidth);
                        System.Console.WriteLine();
                        InputHelper.ShowInfo($"Total: {_manager.CalculateTotal()}".PadLeft(tableWidth));
                        InputHelper.ShowInfo(new string('-', tableWidth));
                    }
                    catch (EmptyListException e)
                    {
                        InputHelper.ShowWarning(e.Message);
                    }
                    break;
                case "0":
                    InputHelper.ShowInfo("Thank you for using this application");
                    run = false;
                    break;

                default:
                    InputHelper.ShowError("Invalid selection\n");
                    //ShowMainMenu();
                    break;
            }


        }
    }
    public void ListProducts(List<Product> products, int categoryWidth, int nameWidth, int priceWidth)
    {
        //empty list + table formatting
        if (products.Count > 0)
        {
            foreach (Product product in products)
            {
                Console.WriteLine(product.Category.PadRight(categoryWidth) + " " + product.Name.PadRight(nameWidth) + " " + Convert.ToString(product.Price).PadLeft(priceWidth));
            }
        }
        else { throw new EmptyListException(); }
    }
    public (int Category, int Name, int Price) GetColumnWidths(List<Product> products)
    {
        if (products.Count > 0)
        {
            int categoryMax = Math.Max("Category".Length, products.Max(p => p.Category.Length)) + ColumnPadding;
            int nameMax = Math.Max("Product name".Length, products.Max(p => p.Name.Length)) + ColumnPadding;
            int priceMax = Math.Max("Price".Length, products.Max(p => Convert.ToString(p.Price).Length)) + ColumnPadding;
            return (categoryMax, nameMax, priceMax);
        }
        else { throw new EmptyListException(); }
    }

}

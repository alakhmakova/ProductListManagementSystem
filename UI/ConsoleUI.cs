using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;
using ProductListManagementSystem.Services;

namespace ProductListManagementSystem.UI;

// Console UI: menu, input, tables, colors; the only class that prints and catches errors
public class ConsoleUI
{
    // Logic and file storage; readonly, so they are never replaced
    private readonly ProductManager _manager = new();
    private readonly ProductFileStorage _storage = new("products.json");
    private const int SeparatorCount = 2; // spaces between the 3 table columns
    private const int ColumnPadding = 2; // extra spaces added to each column width
    public void Run()
    {
        InputHelper.ShowInfo(new string('*', 24));
        InputHelper.ShowInfo("PRODUCT LIST APPLICATION");
        InputHelper.ShowInfo(new string('*', 24));
        // Load saved products on start
        LoadProductsFromFile();
        bool run = true;
        try
        {
            // Menu-based system: repeat until the user enters 0
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
                    // Add products until the user enters q
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
                                SaveProductsToFile();
                                InputHelper.ShowSuccess("The product was successfully added!\n ");

                            }
                        }
                        catch (UserCancelledException e)
                        {
                            InputHelper.ShowWarning(e.Message);
                        }
                        break;
                    // Search by name or category until the user enters q
                    case "2":

                        if (!_manager.HasProducts)
                        {
                            InputHelper.ShowWarning("No products to search yet. Add a product first.");
                        }
                        else
                        {
                            InputHelper.ShowInfo(new string('-', 63));
                            InputHelper.ShowInfo("Search by product name or category | To quit - enter 'Q'");
                            InputHelper.ShowInfo(new string('-', 63));
                            try
                            {
                                while (true)
                                {
                                    try
                                    {
                                        string search = InputHelper.ReadTextOrCancel("Search: ", "Search string can't be empty");
                                        DisplayProductsTable(_manager.SearchProduct(search));
                                    }
                                    catch (ProductsNotFoundException e) { InputHelper.ShowWarning(e.Message); }
                                }
                            }
                            catch (UserCancelledException e) { InputHelper.ShowWarning(e.Message); }



                        }
                        break;
                    // Sorted list with the total price at the bottom
                    case "3":
                        if (_manager.HasProducts)
                        {
                            DisplayProductsTable(_manager.GetProductsSortedByPrice(), _manager.CalculateTotal());
                        }
                        else { InputHelper.ShowWarning("Your product list is empty."); }
                        break;
                    case "0":
                        InputHelper.ShowInfo("Thank you for using this application");
                        run = false;
                        break;

                    // Invalid menu option
                    default:
                        InputHelper.ShowError("Invalid menu option. Select 0-3.");
                        break;
                }


            }
        }
        // Rare case in interactive use: Console.ReadLine() returns null only when the input stream is closed:
        // - the user presses Ctrl+Z and then Enter (Windows) or Ctrl+D (Linux, macOS);
        // - the input is redirected from a text file instead of the keyboard (dotnet run < input.txt)
        catch (InputClosedException e)
        {
            InputHelper.ShowWarning(e.Message);
        }
    }

    // Print products as a table; shows the Total line only if total is given
    private void DisplayProductsTable(List<Product> products, decimal? total = null)
    {
        var widths = GetColumnWidths(products);
        int tableWidth = widths.Category + widths.Name + widths.Price + SeparatorCount;
        InputHelper.ShowInfo(new string('-', tableWidth));
        InputHelper.ShowInfo("Category".PadRight(widths.Category) + " " + "Product name".PadRight(widths.Name) + " " + "Price".PadLeft(widths.Price));
        InputHelper.ShowInfo(new string('-', tableWidth));
        ListProducts(products, widths.Category, widths.Name, widths.Price);
        InputHelper.ShowInfo(new string('-', tableWidth));

        if (total.HasValue)
        {
            InputHelper.ShowInfo($"Total: {total.Value}".PadLeft(tableWidth));
            InputHelper.ShowInfo(new string('-', tableWidth));
        }
    }

    // One row per product
    private void ListProducts(List<Product> products, int categoryWidth, int nameWidth, int priceWidth)
    {
        foreach (Product product in products)
        {
            Console.WriteLine(product.Category.PadRight(categoryWidth) + " " + product.Name.PadRight(nameWidth) + " " + Convert.ToString(product.Price).PadLeft(priceWidth));
        }

    }
    // Column width = longest value or header + padding
    private (int Category, int Name, int Price) GetColumnWidths(List<Product> products)
    {
        int categoryMax = Math.Max("Category".Length, products.Max(p => p.Category.Length)) + ColumnPadding;
        int nameMax = Math.Max("Product name".Length, products.Max(p => p.Name.Length)) + ColumnPadding;
        int priceMax = Math.Max("Price".Length, products.Max(p => Convert.ToString(p.Price).Length)) + ColumnPadding;
        return (categoryMax, nameMax, priceMax);

    }

    // Load products from the file; on error start with an empty list
    private void LoadProductsFromFile()
    {
        try
        {
            List<Product> productsLoaded = _storage.Load();
            _manager.LoadProducts(productsLoaded);
            if (productsLoaded.Count > 0)
            {
                InputHelper.ShowSuccess($"{productsLoaded.Count} products loaded from file");
            }
            else
            {
                InputHelper.ShowSuccess("No saved products found. Starting with an empty list.");
            }
        }
        catch (DataFileException e) { InputHelper.ShowWarning(e.Message + " Starting with an empty list."); }
    }

    // Save all products; show an error if the file can't be written
    private void SaveProductsToFile()
    {
        try
        {
            _storage.Save(_manager.GetProductsSortedByPrice());
        }
        catch (DataFileException e) { InputHelper.ShowError(e.Message); }
    }

}

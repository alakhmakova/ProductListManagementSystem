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
    private const int SeparatorCount = 3; // spaces between the 4 table columns
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
                Console.WriteLine("4-Delete a Product");
                Console.WriteLine("0-Quit");

                string menuOption = InputHelper.ReadText("Select Option 0-4: ", "Menu option can't be empty");

                switch (menuOption)
                {
                    // Add products until the user enters q
                    case "1":
                        ShowHeader("To enter a new product - follow the steps | To quit - enter 'Q'");
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
                            ShowHeader("Search by product name or category | To quit - enter 'Q'");
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
                    // Delete by id: show the product, ask to confirm; repeat until the user enters q
                    case "4":
                        if (!_manager.HasProducts)
                        {
                            InputHelper.ShowWarning("No products to delete yet. Add a product first.");
                        }
                        else
                        {
                            ShowHeader("Enter the id of the product to delete | To quit - enter 'Q'");
                            try
                            {
                                // Stop when there is nothing left to delete
                                while (_manager.HasProducts)
                                {
                                    try
                                    {
                                        int id = InputHelper.ReadId("Enter product id to delete: ", "Product id can't be empty", "Product id must be a whole number greater than 0");
                                        Product product = _manager.GetProductById(id);
                                        DisplayProductsTable(new List<Product> { product });
                                        if (InputHelper.ReadYesNo("Delete this product? (y/n): ", "Answer can't be empty", "Please enter y or n"))
                                        {
                                            _manager.DeleteProductById(id);
                                            SaveProductsToFile();
                                            InputHelper.ShowSuccess("The product was successfully deleted!\n ");
                                        }
                                        else
                                        {
                                            InputHelper.ShowWarning("The product was not deleted.\n ");
                                        }
                                    }
                                    catch (ProductsNotFoundException e) { InputHelper.ShowWarning(e.Message); }
                                }
                                InputHelper.ShowWarning("Your product list is empty.");
                            }
                            catch (UserCancelledException e) { InputHelper.ShowWarning(e.Message); }
                        }
                        break;
                    case "0":
                        InputHelper.ShowInfo("Thank you for using this application");
                        run = false;
                        break;

                    // Invalid menu option
                    default:
                        InputHelper.ShowError("Invalid menu option. Select 0-4.");
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

    // Text between two dash lines of the same length
    private void ShowHeader(string text)
    {
        InputHelper.ShowInfo(new string('-', text.Length));
        InputHelper.ShowInfo(text);
        InputHelper.ShowInfo(new string('-', text.Length));
    }

    // Print products as a table; shows the Total line only if total is given
    private void DisplayProductsTable(List<Product> products, decimal? total = null)
    {
        var widths = GetColumnWidths(products);
        int tableWidth = widths.Id + widths.Category + widths.Name + widths.Price + SeparatorCount;
        InputHelper.ShowInfo(new string('-', tableWidth));
        InputHelper.ShowInfo("Id".PadRight(widths.Id) + " " + "Category".PadRight(widths.Category) + " " + "Product name".PadRight(widths.Name) + " " + "Price".PadLeft(widths.Price));
        InputHelper.ShowInfo(new string('-', tableWidth));
        ListProducts(products, widths.Id, widths.Category, widths.Name, widths.Price);
        InputHelper.ShowInfo(new string('-', tableWidth));

        if (total.HasValue)
        {
            InputHelper.ShowInfo($"Total: {total.Value}".PadLeft(tableWidth));
            InputHelper.ShowInfo(new string('-', tableWidth));
        }
    }

    // One row per product
    private void ListProducts(List<Product> products, int idWidth, int categoryWidth, int nameWidth, int priceWidth)
    {
        foreach (Product product in products)
        {
            Console.WriteLine(product.Id.ToString().PadRight(idWidth) + " " + product.Category.PadRight(categoryWidth) + " " + product.Name.PadRight(nameWidth) + " " + Convert.ToString(product.Price).PadLeft(priceWidth));
        }

    }
    // Column width = longest value or header + padding
    private (int Id, int Category, int Name, int Price) GetColumnWidths(List<Product> products)
    {
        int idMax = Math.Max("Id".Length, products.Max(p => p.Id.ToString().Length)) + ColumnPadding;
        int categoryMax = Math.Max("Category".Length, products.Max(p => p.Category.Length)) + ColumnPadding;
        int nameMax = Math.Max("Product name".Length, products.Max(p => p.Name.Length)) + ColumnPadding;
        int priceMax = Math.Max("Price".Length, products.Max(p => Convert.ToString(p.Price).Length)) + ColumnPadding;
        return (idMax, categoryMax, nameMax, priceMax);

    }

    // Load products from the file; on error start with an empty list
    private void LoadProductsFromFile()
    {
        try
        {
            ProductData data = _storage.Load();
            _manager.LoadData(data);
            if (data.Products.Count > 0)
            {
                InputHelper.ShowSuccess($"{data.Products.Count} products loaded from file");
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
            _storage.Save(_manager.GetData());
        }
        catch (DataFileException e) { InputHelper.ShowError(e.Message); }
    }

}

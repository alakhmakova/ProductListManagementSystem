using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;
using System.Text.Json;

namespace ProductListManagementSystem.Services;

// Saves and loads products as JSON; file errors become DataFileException
public class ProductFileStorage
{
    private readonly string _filePath;
    // Readable JSON: line breaks and indentation
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public ProductFileStorage(string filePath)
    {
        _filePath = filePath;
    }

    // Write all products to the file (serialization)
    public void Save(List<Product> products)
    {
        try
        {
            string json = JsonSerializer.Serialize(products, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }
        catch (IOException e) { throw new DataFileException("Could not save products.", e); }
        catch (UnauthorizedAccessException e) { throw new DataFileException("Could not save products. Access denied.", e); }
    }
    // Read products from the file (deserialization)
    public List<Product> Load()
    {
        try
        {
            if (!File.Exists(_filePath)) // first run: no file yet
            {
                return new();
            }

            string fromFile = File.ReadAllText(_filePath);
            // The Product constructor runs here, so invalid data is rejected
            List<Product>? loaded = JsonSerializer.Deserialize<List<Product>>(fromFile);
            return loaded ?? new(); // "null" in the file -> empty list
        }
        catch (JsonException e) { throw new DataFileException("Could not load products. The data file is corrupted.", e); }
        catch (IOException e) { throw new DataFileException("Could not load products.", e); }
        catch (UnauthorizedAccessException e) { throw new DataFileException("Could not load products. Access denied.", e); }
        catch (ValidationException e) { throw new DataFileException("Could not load products. The file contains an invalid product.", e); }

    }
}

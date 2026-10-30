using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;
using System.Text.Json;

namespace ProductListManagementSystem.Services;

public class ProductFileStorage
{
    private readonly string _filePath;
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public ProductFileStorage(string filePath)
    {
        _filePath = filePath;
    }

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
    public List<Product> Load()
    {
        try
        {
            if (!File.Exists(_filePath)) //return empty list if there is no file 
            {
                return new();
            }

            string fromFile = File.ReadAllText(_filePath);
            List<Product>? loaded = JsonSerializer.Deserialize<List<Product>>(fromFile);
            return loaded ?? new();
        }
        catch (JsonException e) { throw new DataFileException("Could not load products. The data file is corrupted.", e); }
        catch (IOException e) { throw new DataFileException("Could not load products.", e); }
        catch (UnauthorizedAccessException e) { throw new DataFileException("Could not load products. Access denied.", e); }
        catch (ValidationException e) { throw new DataFileException("Could not load products. The file contains an invalid product.", e); }

    }
}

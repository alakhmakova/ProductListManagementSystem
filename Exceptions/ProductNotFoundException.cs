using System;

namespace ProductListManagementSystem.Exceptions;
// How to create user defined exceptions https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
public class ProductNotFoundException : Exception
{
    public ProductNotFoundException()
        : base("Product not found.\n") { }

    public ProductNotFoundException(string message)
        : base(message) { }

    public ProductNotFoundException(string message, Exception innerException)
        : base(message, innerException) { }
}

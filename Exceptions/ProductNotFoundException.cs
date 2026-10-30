using System;

namespace ProductListManagementSystem.Exceptions;
// How to create user defined exceptions https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
public class ProductsNotFoundException : Exception
{
    public ProductsNotFoundException()
        : base("Products not found.\n") { }

    public ProductsNotFoundException(string message)
        : base(message) { }

    public ProductsNotFoundException(string message, Exception innerException)
        : base(message, innerException) { }
}

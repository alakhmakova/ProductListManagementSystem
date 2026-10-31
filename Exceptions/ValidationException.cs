namespace ProductListManagementSystem.Exceptions;
// How to create user defined exceptions https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
public class ValidationException : Exception
{
    public ValidationException()
        : base("Invalid product.") { }

    public ValidationException(string message)
        : base(message) { }

    public ValidationException(string message, Exception innerException)
        : base(message, innerException) { }
}

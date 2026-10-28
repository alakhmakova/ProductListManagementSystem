using System;

namespace ProductListManagementSystem.Exceptions;
// How to create user defined exceptions https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
public class InputClosedException : Exception
{
    public InputClosedException()
        : base("Input stream was closed. No more input can be read.\n") { }

    public InputClosedException(string message)
        : base(message) { }

    public InputClosedException(string message, Exception innerException)
        : base(message, innerException) { }
}

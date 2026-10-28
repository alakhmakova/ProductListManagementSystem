using System;

namespace ProductListManagementSystem.Exceptions;

public class EmptyListException : Exception
{
    public EmptyListException()
        : base("Your product list is empty\n") { }

    public EmptyListException(string message)
        : base(message) { }

    public EmptyListException(string message, Exception innerException)
        : base(message, innerException) { }
}

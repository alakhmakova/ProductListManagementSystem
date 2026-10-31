using System;

namespace ProductListManagementSystem.Exceptions;

public class UserCancelledException : Exception
{
    public UserCancelledException()
        : base("User cancelled the operation.") { }

    public UserCancelledException(string message)
        : base(message) { }

    public UserCancelledException(string message, Exception innerException)
        : base(message, innerException) { }
}

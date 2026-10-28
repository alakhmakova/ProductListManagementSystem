using System;

namespace ProductListManagementSystem.Exceptions;

public class UserCancelledException : Exception
{
    public UserCancelledException()
        : base("User cancelled the operation.\n") { }

    public UserCancelledException(string message)
        : base(message) { }

    public UserCancelledException(string message, Exception innerException)
        : base(message, innerException) { }
}

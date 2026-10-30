namespace ProductListManagementSystem.Exceptions;

public class DataFileException : Exception
{
    public DataFileException()
        : base("Data file error.\n") { }

    public DataFileException(string message)
        : base(message) { }

    public DataFileException(string message, Exception innerException)
        : base(message, innerException) { }
}

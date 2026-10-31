namespace ProductListManagementSystem.Exceptions;

// Thrown when the data file can't be read or written
public class DataFileException : Exception
{
    public DataFileException()
        : base("Data file error.") { }

    public DataFileException(string message)
        : base(message) { }

    public DataFileException(string message, Exception innerException)
        : base(message, innerException) { }
}

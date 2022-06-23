namespace WebServer.Exceptions;

public class NewServiceException : Exception
{
    public NewServiceException() : base("Service property error")
    {
        Error = 0;
    }

    public NewServiceException(int error) : this()
    {
        Error = error;
    }

    public int Error { get; }
}
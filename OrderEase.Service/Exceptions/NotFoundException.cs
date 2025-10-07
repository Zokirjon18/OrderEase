namespace OrderEase.Service.Exceptions;

public class NotFoundException : Exception
{
    public int StatusCode { get; set; }
    public NotFoundException(string message) : base("Not found exception: " + message)
    {
        StatusCode = 404;
    }
}
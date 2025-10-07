namespace OrderEase.Service.Exceptions;

public class AlreadyExistsException : Exception
{
    public int StatusCode { get; set; }

    public AlreadyExistsException(string message) : base("Already exits exception: " + message)
    {
        StatusCode = 403;
    }
}

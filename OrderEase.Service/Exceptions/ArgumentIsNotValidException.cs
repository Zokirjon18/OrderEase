namespace OrderEase.Service.Exceptions;

public class ArgumentIsNotValidException : Exception
{
    public int StatusCode { get; set; }
    public ArgumentIsNotValidException(string message) : base("Argument is not valid exception: " + message)
    {
        StatusCode = 400;
    }
}

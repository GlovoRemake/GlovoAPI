namespace Core.Dtos.Exceptions.Account;

public class AnotherTypeRegException : Exception
{
    public AnotherTypeRegException()
        : base("") { }

    public AnotherTypeRegException(string message)
        : base(message) { }
}
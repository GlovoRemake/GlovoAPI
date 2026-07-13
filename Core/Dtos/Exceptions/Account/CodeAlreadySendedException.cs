namespace Core.Dtos.Exceptions.Account;

public class CodeAlreadySendedException : Exception
{
    public CodeAlreadySendedException()
        : base("") { }

    public CodeAlreadySendedException(string message)
        : base(message) { }
}
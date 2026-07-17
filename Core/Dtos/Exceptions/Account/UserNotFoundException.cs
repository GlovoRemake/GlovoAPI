namespace Core.Dtos.Exceptions.Account;

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
        : base("") { }

    public UserNotFoundException(string message)
        : base(message) { }
}

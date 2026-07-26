namespace Core.Dtos.Exceptions.Account;

public class ChangePasswordException : Exception
{
    public ChangePasswordException()
         : base("") { }

    public ChangePasswordException(string message)
         : base(message) { }
}

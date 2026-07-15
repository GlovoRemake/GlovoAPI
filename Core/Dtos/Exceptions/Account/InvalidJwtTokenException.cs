namespace Core.Dtos.Exceptions.Account;

public class InvalidJwtTokenException : Exception
{
	public InvalidJwtTokenException()
        : base("") { }

    public InvalidJwtTokenException(string message)
        : base(message) { }
}

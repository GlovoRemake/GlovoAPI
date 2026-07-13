using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account;

public class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException()
        : base("") { }
    public InvalidRefreshTokenException(string message)
        : base(message) { }
}

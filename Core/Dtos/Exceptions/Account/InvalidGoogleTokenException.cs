using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account;

public class InvalidGoogleTokenException : Exception
{
    public InvalidGoogleTokenException()
        : base("") { }
    public InvalidGoogleTokenException(string message)
        : base(message) { }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account;

public class BadCodeException : Exception
{
    public BadCodeException()
        : base("") { }

    public BadCodeException (string message)
        : base(message) { }
}

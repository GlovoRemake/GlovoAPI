using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account;

public class ExpiredCodeException : Exception
{
    public ExpiredCodeException()
        : base("") { }

    public ExpiredCodeException(string message)
        : base(message) { }
}

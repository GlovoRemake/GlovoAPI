using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account.Cart;

public class InvalidAdditionalException : Exception
{
    public InvalidAdditionalException()
        : base("") { }

    public InvalidAdditionalException(string message)
        : base(message) { }
}
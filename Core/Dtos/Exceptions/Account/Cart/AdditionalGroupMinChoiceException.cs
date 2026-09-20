using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account.Cart;
public class AdditionalGroupMinChoiceException : Exception
{
    public AdditionalGroupMinChoiceException()
        : base("") { }

    public AdditionalGroupMinChoiceException(string message)
        : base(message) { }
}
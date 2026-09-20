using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account.Cart;
public class AdditionalGroupMaxChoiceException : Exception
{
    public AdditionalGroupMaxChoiceException()
        : base("") { }

    public AdditionalGroupMaxChoiceException(string message)
        : base(message) { }
}
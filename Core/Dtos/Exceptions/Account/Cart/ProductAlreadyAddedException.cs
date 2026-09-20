using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account.Cart;
public class ProductAlreadyAddedException : Exception
{
    public ProductAlreadyAddedException()
        : base("") { }

    public ProductAlreadyAddedException(string message)
        : base(message) { }
}
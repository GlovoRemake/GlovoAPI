using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account.Cart;

public class CartItemNotFoundException : Exception
{
    public CartItemNotFoundException()
        : base("") { }

    public CartItemNotFoundException(string message)
        : base(message) { }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account.Order;
public class OrderIsAnavaibleException : Exception
{
    public OrderIsAnavaibleException()
        : base("") { }

    public OrderIsAnavaibleException(string message)
        : base(message) { }
}

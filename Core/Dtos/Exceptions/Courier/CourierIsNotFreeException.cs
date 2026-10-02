using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Courier;

public class CourierIsNotFreeException : Exception
{
    public CourierIsNotFreeException()
        : base("") { }
    public CourierIsNotFreeException(string message)
        : base(message) { }
}

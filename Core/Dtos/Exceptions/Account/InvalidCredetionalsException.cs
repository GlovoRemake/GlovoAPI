using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account;

public class InvalidCredetionalsException : Exception
{
    public InvalidCredetionalsException()
        : base("") { }
    public InvalidCredetionalsException(string message)
        : base(message) { }
}

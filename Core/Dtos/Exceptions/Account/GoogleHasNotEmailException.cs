using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account;

public class GoogleHasNotEmailException : Exception
{
    public GoogleHasNotEmailException()
        : base("") { }
    public GoogleHasNotEmailException(string message)
        : base(message) { }
}

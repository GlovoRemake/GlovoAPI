using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account;

public class UserAlreadyRegisteredException : Exception
{
    public UserAlreadyRegisteredException()
        : base("") { }
    public UserAlreadyRegisteredException(string message)
        : base(message) { }
}

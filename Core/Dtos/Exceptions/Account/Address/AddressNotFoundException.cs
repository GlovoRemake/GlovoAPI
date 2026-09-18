using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Account.Address;
public class AddressNotFoundException : Exception
{
    public AddressNotFoundException()
        : base("") { }

    public AddressNotFoundException(string message)
        : base(message) { }
}
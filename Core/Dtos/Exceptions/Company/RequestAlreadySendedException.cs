using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Company;

public class RequestAlreadySendedException : Exception
{
    public RequestAlreadySendedException()
        : base("") { }

    public RequestAlreadySendedException(string message)
        : base(message) { }
}
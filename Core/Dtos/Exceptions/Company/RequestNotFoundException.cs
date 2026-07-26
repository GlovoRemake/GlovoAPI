using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Company;
public class RequestNotFoundException : Exception
{
    public RequestNotFoundException()
        : base("") { }

    public RequestNotFoundException(string message)
        : base(message) { }
}
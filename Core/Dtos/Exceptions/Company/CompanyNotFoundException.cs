using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Company;

public class CompanyNotFoundException : Exception
{
    public CompanyNotFoundException()
        : base("") { }

    public CompanyNotFoundException(string message)
        : base(message) { }
}
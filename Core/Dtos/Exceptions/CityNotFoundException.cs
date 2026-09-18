using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions;
public class CityNotFoundException : Exception
{
    public CityNotFoundException()
        : base("") { }
    public CityNotFoundException(string message)
        : base(message) { }
}

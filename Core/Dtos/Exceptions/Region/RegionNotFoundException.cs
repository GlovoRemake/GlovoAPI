using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Region;
public class RegionNotFoundException : Exception
{
    public RegionNotFoundException()
        : base("") { }
    public RegionNotFoundException(string message)
        : base(message) { }
}

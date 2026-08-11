using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Partner;

public class PartnerNotFound : Exception
{
    public PartnerNotFound()
        : base("") { }
    public PartnerNotFound(string message)
        : base(message) { }
}

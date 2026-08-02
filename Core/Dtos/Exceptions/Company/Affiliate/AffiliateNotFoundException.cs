using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Company.Affiliate;

public class AffiliateNotFoundException : Exception
{
    public AffiliateNotFoundException()
        : base("") { }

    public AffiliateNotFoundException(string message)
        : base(message) { }
}

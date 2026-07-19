using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Exceptions.Partner;

public class EmailNotConfirmed : Exception
{
    public EmailNotConfirmed()
        : base("") { }
    public EmailNotConfirmed(string message)
        : base(message) { }
}

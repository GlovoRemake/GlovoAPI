using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account;

public class GoogleLoginRequest
{
    public string IdToken { get; set; } = string.Empty;
}

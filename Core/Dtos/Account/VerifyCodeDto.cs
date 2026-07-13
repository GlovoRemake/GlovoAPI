using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account;

public class VerifyCodeDto
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

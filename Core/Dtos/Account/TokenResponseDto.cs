using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account;

public class TokenResponseDto
{
    public bool RequiresRegistration { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
}

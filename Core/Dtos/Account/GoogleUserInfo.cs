using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account;

public class GoogleUserInfo
{
    public string Sub { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string GivenName { get; set; } = null!;
    public string FamilyName { get; set; } = null!;
    public string Picture { get; set; } = null!;
}

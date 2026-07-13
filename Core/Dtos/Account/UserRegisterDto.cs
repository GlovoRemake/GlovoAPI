using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account;

public class UserRegisterDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

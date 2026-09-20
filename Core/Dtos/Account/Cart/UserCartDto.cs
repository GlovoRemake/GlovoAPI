using Core.Dtos.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Cart;

public class UserCartDto
{
    public CompanyDto Company { get; set; } = default!;
    public List<CartDto> Carts { get; set; } = default!;
}

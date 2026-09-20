using Core.Dtos.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Cart;

public class CartItemDto
{
    public CompanyDto Company { get; set; } = default!;
    public CartDto Cart { get; set; } = default!;
}
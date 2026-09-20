using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Cart;

public class UpdateCartDto
{
    public int Count { get; set; }
    public List<int>? AdditionalIds { get; set; }
}
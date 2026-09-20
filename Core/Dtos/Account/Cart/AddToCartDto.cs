using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Cart;

public class AddToCartDto
{
    public int Count { get; set; }
    public int ProductId { get; set; }
    public List<int> AdditionalIds { get; set; } = default!;
}

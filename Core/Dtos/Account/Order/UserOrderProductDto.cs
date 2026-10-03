using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Order;

public class UserOrderProductDto
{
    public int ProductId { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }

    public List<UserOrderAdditionalDto> Additionals { get; set; } = [];
}

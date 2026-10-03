using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Order;

public class UserOrderAdditionalDto
{
    public int AdditionalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
}


using Core.Dtos.Company.Product.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Cart;

public class CartAdditionalGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinChoice { get; set; }
    public int MaxChoice { get; set; }
    public int Order { get; set; }
    public List<CartAdditionalDto> Additionals { get; set; } = default!;
}

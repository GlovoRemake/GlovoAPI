using Core.Dtos.Company.Product;
using Core.Dtos.Company.Product.AdditionalGroup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Cart;

public class CartDto
{
    public int Id { get; set; }
    public int Count { get; set; }
    public ProductDto Product { get; set; } = default!;
    public List<CartAdditionalGroupDto> AdditionalGroups { get; set; } = default!;
}

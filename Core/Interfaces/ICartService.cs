using Core.Dtos.Account.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface ICartService
{
    Task<List<UserCartDto>> GetCarts(Guid userId);
    Task AddToCart(Guid userId, AddToCartDto dto);
    Task UpdateCart(Guid userId, int cartId, UpdateCartDto dto);
    Task RemoveFromCart(Guid userId, int cartId);
    Task RemoveAllCart(Guid userId);
}

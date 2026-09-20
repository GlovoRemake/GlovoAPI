using Core.Commands.Account.Cart;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account.Cart;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account.Cart;

public sealed class DeleteFromCartCommandHandler
    : IRequestHandler<DeleteFromCartCommand, Result>
{
    private readonly ICartService _cartService;

    public DeleteFromCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<Result> Handle(
        DeleteFromCartCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _cartService.RemoveFromCart(request.userId, request.cartId);
        }
        catch (CartItemNotFoundException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "CartItemNotFound",
                $"Товар не знайдено в кошику"
            ));
        }
        catch (Exception ex)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during login: {ex.Message}"
            ));
        }

        return Result.Success();
    }
}
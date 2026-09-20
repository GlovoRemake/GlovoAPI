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

public sealed class DeleteAllCartCommandHandler
    : IRequestHandler<DeleteAllCartCommand, Result>
{
    private readonly ICartService _cartService;

    public DeleteAllCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<Result> Handle(
        DeleteAllCartCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _cartService.RemoveAllCart(request.userId);
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
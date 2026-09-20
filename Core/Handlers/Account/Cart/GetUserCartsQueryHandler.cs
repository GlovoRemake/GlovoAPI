using Core.Commands.Account.Cart;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Cart;
using Core.Dtos.Exceptions.Account.Cart;
using Core.Interfaces;
using Core.Queries.Account.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account.Cart;
public sealed class GetUserCartsQueryHandler
    : IRequestHandler<GetUserCartsQuery, Result<List<UserCartDto>>>
{
    private readonly ICartService _cartService;

    public GetUserCartsQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<Result<List<UserCartDto>>> Handle(
        GetUserCartsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var res = await _cartService.GetCarts(request.userId);
            return Result<List<UserCartDto>>.Success(res);
        }
        catch (Exception ex)
        {
            return Result<List<UserCartDto>>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during login: {ex.Message}"
            ));
        }
    }
}
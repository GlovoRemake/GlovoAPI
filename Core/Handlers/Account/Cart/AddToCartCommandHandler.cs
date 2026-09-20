using Core.Commands.Account;
using Core.Commands.Account.Cart;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Exceptions.Account.Cart;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account.Cart;

public sealed class AddToCartCommandHandler
    : IRequestHandler<AddToCartCommand, Result>
{
    private readonly ICartService _cartService;

    public AddToCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<Result> Handle(
        AddToCartCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _cartService.AddToCart(request.userId, request.dto);
        }
        catch (ProductAlreadyAddedException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "ProductAlreadyAdded",
                $"Товар вже додано до кошика"
            ));
        }
        catch (InvalidAdditionalException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "InvalidAdditional",
                $"Невірні додаткові дані"
            ));
        }
        catch (AdditionalGroupMinChoiceException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "AdditionalGroupMinChoice",
                $"Невірна вибірка додаткових груп"
            ));
        }
        catch (AdditionalGroupMaxChoiceException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "AdditionalGroupMaxChoice",
                $"Невірна вибірка додаткових груп"
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
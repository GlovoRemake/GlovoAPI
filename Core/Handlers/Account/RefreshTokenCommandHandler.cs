using Core.Commands.Account;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account;

public sealed class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
{
    private readonly IAccountService _accountService;

    public RefreshTokenCommandHandler(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
    }

    public async Task<Result<TokenResponseDto>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        TokenResponseDto res;

        try
        {
            res = await _accountService.RefreshToken(request.refreshToken);

        }
        catch (InvalidRefreshTokenException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "Token",
                $"Невірний токен оновлення"
            ));
        }
        catch (Exception ex)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during token refresh: {ex.Message}"
            ));
        }

        return Result<TokenResponseDto>.Success(res);
    }
}
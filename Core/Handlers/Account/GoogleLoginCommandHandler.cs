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

public sealed class GoogleLoginCommandHandler
    : IRequestHandler<GoogleLoginCommand, Result<TokenResponseDto>>
{
    private readonly IAccountService _accountService;

    public GoogleLoginCommandHandler(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
    }

    public async Task<Result<TokenResponseDto>> Handle(
        GoogleLoginCommand request,
        CancellationToken cancellationToken)
    {
        TokenResponseDto res;

        try
        {
            res = await _accountService.GoogleLoginAsync(request.request);

        }
        catch (InvalidGoogleTokenException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "GoogleToken",
                $"Невірний токен Google"
            ));
        }
        catch (GoogleHasNotEmailExcention)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "GoogleEmail",
                $"Ваш google акаунт не має прив'язаної email адреси"
            ));
        }
        catch (AnotherTypeRegException ex)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "AnotherTypeReg",
                $"Користувач зареєстрований іншим способом ({ex.Message})"
            ));
        }
        catch (Exception ex)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during login: {ex.Message}"
            ));
        }

        return Result<TokenResponseDto>.Success(res);
    }
}
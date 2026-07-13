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

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, Result<TokenResponseDto>>
{
    private readonly IAccountService _accountService;

    public LoginCommandHandler(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
    }

    public async Task<Result<TokenResponseDto>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        TokenResponseDto res;

        try
        {
            res = await _accountService.LoginAsync(request.dto.Email, request.dto.Password);

        }
        catch (NullReferenceException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "LoginError",
                $"Пошта або пароль неправильні"
            ));
        }
        catch (InvalidCredetionalsException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "LoginError",
                $"Пошта або пароль неправильні"
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
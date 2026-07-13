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

public sealed class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, Result<TokenResponseDto>>
{
    private readonly IAccountService _accountService;

    public RegisterCommandHandler(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
    }

    public async Task<Result<TokenResponseDto>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        TokenResponseDto res;

        try
        {
            res = await _accountService.RegisterAsync(request.email, request.dto);

        }
        catch (UserAlreadyRegisteredException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "Email",
                $"Пошта вже зареєстрована"
            ));
        }
        catch (Exception ex)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }

        return Result<TokenResponseDto>.Success(res);
    }
}
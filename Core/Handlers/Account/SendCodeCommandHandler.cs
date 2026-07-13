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

public sealed class SendCodeCommandHandler
    : IRequestHandler<SendCodeCommand, Result>
{
    private readonly IAccountService _accountService;

    public SendCodeCommandHandler(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
    }

    public async Task<Result> Handle(
        SendCodeCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _accountService.SendVerificationCodeAsync(request.dto);

        }
        catch (AnotherTypeRegException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "AnotherTypeReg",
                $"Користувач зареєстрований іншим способом ({ex.Message})"
            ));
        }
        catch (CodeAlreadySendedException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "CodeAlreadySended",
                $"{ex.Message}"
            ));
        }
        catch (Exception ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during token refresh: {ex.Message}"
            ));
        }

        return Result.Success();
    }
}
using Core.Commands.Account;
using Core.Dtos;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Core.Handlers.Account;

public sealed class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
{
    private readonly IAccountService _accountService;

    public ForgotPasswordCommandHandler(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
    }

    public async Task<Result> Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _accountService.ForgotPasswordAsync(request.dto.Email);

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
        catch (UserNotFoundException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "User",
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

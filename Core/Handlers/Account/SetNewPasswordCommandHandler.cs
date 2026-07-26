using Core.Commands.Account;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Core.Handlers.Account;

public sealed class SetNewPasswordCommandHandler
    : IRequestHandler<SetNewPasswordCommand, Result>
{
    private readonly IAccountService _accountService;

    public SetNewPasswordCommandHandler(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
    }

    public async Task<Result> Handle(SetNewPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _accountService.SetNewPasswordAsync(request.email, request.dto);
        }
        catch (InvalidCredetionalsException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "Email",
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
        catch (ChangePasswordException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "Password",
                $"{ex.Message}"
            ));
        }
        catch (Exception ex)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during setting new password: {ex.Message}"
            ));
        }
        return Result.Success();
    }
    
}

using Core.Commands.Account;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Core.Handlers.Account;

public sealed class VerifyResetCodeCommandHandler
    : IRequestHandler<VerifyResetCodeCommand, Result<TokenResponseDto>>
{
    private readonly IAccountService _accountService;

    public VerifyResetCodeCommandHandler(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
    }

    public async Task<Result<TokenResponseDto>> Handle(
        VerifyResetCodeCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var token = await _accountService.VerifyResetCode(request.dto);
            return Result<TokenResponseDto>.Success(token);
        }
        catch (BadCodeException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "BadCode",
                $"Невірний код"
            ));
        }
        catch (ExpiredCodeException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "ExpiredCode",
                $"Код вже не дійсний"
            ));
        }
        catch (Exception ex)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during token refresh: {ex.Message}"
            ));
        }
    }
}

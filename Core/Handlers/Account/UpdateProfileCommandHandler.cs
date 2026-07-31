using Core.Commands.Account;
using Core.Dtos;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Core.Handlers.Account;

public sealed class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result>
{
    private readonly IAccountService _accountService;

    public UpdateProfileCommandHandler(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
    }
    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _accountService.UpdateProfileAsync(request.email, request.dto);
        }
        catch (AnotherTypeRegException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "AnotherTypeReg",
                $"Користувач зареєстрований іншим способом ({ex.Message})"
            ));
        }
        catch (UserNotFoundException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "User",
                $"{ex.Message}"
            ));
        }
        catch (Exception)
        {

            throw;
        }
        return Result.Success();
    }
}

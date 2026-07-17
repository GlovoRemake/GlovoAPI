using Core.Commands.Account;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Account;

public sealed class GetProfileCommandHandler
    : IRequestHandler<GetProfileCommand, Result<GetProfileDto>>
{
    private readonly IAccountService _accountService;

    public GetProfileCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public async Task<Result<GetProfileDto>> Handle(GetProfileCommand request, CancellationToken cancellationToken)
    {
		try
		{
            GetProfileDto res = await _accountService.GetProfile(request.userId);
            return Result<GetProfileDto>.Success(res);
        }
        catch (UserNotFoundException ex)
        {
            return Result<GetProfileDto>.Failure(ErrorMessage.Create(
                "UserNotFound",
                ex.Message
            ));
        }
        catch (InvalidJwtTokenException ex)
        {
            return Result<GetProfileDto>.Failure(ErrorMessage.Create(
                "InvalidJwtToken",
                ex.Message
            ));
        }
        catch (Exception ex)
        {
            return Result<GetProfileDto>.Failure(ErrorMessage.Create(
                "Exception",
                ex.Message
            ));
        }
    }
}

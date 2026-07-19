using Core.Commands.Partner;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Partner;

public sealed class VerifyPartnerCodeCommandHandler
    : IRequestHandler<VerifyPartnerCodeCommand, Result<TokenResponseDto>>
{
    private readonly IPartnerService _partnerService;

    public VerifyPartnerCodeCommandHandler(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    public async Task<Result<TokenResponseDto>> Handle(
        VerifyPartnerCodeCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var token = await _partnerService.VerifyPartnerCode(request.dto);
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
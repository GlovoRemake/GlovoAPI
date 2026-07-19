using Core.Commands.Partner;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Partner;


public sealed class PartnerRefreshTokenCommandHandler
    : IRequestHandler<PartnerRefreshTokenCommand, Result<TokenResponseDto>>
{
    private readonly IPartnerService _partnerService;

    public PartnerRefreshTokenCommandHandler(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    public async Task<Result<TokenResponseDto>> Handle(
        PartnerRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        TokenResponseDto res;

        try
        {
            res = await _partnerService.RefreshToken(request.refreshToken);

        }
        catch (InvalidRefreshTokenException)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "Token",
                $"Невірний токен оновлення"
            ));
        }
        catch (Exception ex)
        {
            return Result<TokenResponseDto>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during token refresh: {ex.Message}"
            ));
        }

        return Result<TokenResponseDto>.Success(res);
    }
}
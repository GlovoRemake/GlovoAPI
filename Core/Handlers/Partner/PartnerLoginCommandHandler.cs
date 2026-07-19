using Core.Commands.Partner;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Partner;

public sealed class PartnerLoginCommandHandler
    : IRequestHandler<PartnerLoginCommand, Result<TokenResponseDto>>
{
    private readonly IPartnerService _partnerService;

    public PartnerLoginCommandHandler(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    public async Task<Result<TokenResponseDto>> Handle(
        PartnerLoginCommand request,
        CancellationToken cancellationToken)
    {
        TokenResponseDto res;

        try
        {
            res = await _partnerService.PartnerLogin(request.email, request.password);

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
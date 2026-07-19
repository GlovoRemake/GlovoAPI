using Core.Commands.Partner;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Exceptions.Partner;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Partner;

public sealed class PartnerRegisterCommandHandler
    : IRequestHandler<PartnerRegisterCommand, Result>
{
    private readonly IPartnerService _partnerService;

    public PartnerRegisterCommandHandler(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    public async Task<Result> Handle(
        PartnerRegisterCommand request,
        CancellationToken cancellationToken)
    {
        TokenResponseDto res;

        try
        {
            await _partnerService.PartnerRegister(request.dto);
        }
        catch (PartnerEmailAlreadyRegistered)
        {
            return Result.Failure(ErrorMessage.Create(
                "Email",
                $"Пошта вже зареєстрована"
            ));
        }
        catch (PartnerPhoneAlreadyRegistered)
        {
            return Result.Failure(ErrorMessage.Create(
                "Phone",
                $"Телефон вже зареєстрований"
            ));
        }
        catch (CodeAlreadySendedException ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "AlreadySended",
                $"{ex.Message}"
            ));
        }
        catch (Exception ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }

        return Result.Success();
    }
}
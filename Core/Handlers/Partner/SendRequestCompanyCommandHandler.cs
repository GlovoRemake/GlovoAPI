using Core.Commands.Partner;
using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Partner;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Partner;

public sealed class SendRequestCompanyCommandHandler
    : IRequestHandler<SendRequestCompanyCommand, Result>
{
    private readonly IPartnerService _partnerService;

    public SendRequestCompanyCommandHandler(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    public async Task<Result> Handle(
        SendRequestCompanyCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _partnerService.SendRequestCompany(request.id, request.dto);
        }
        catch (UserNotFoundException)
        {
            return Result.Failure(ErrorMessage.Create(
                "UserNotFound",
                $"Користувача не знайдено"
            ));
        }
        catch (RequestAlreadySendedException)
        {
            return Result.Failure(ErrorMessage.Create(
                "RequestAlreadySended",
                $"Такий запит вже існує"
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
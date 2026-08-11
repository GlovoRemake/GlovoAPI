using Core.Commands.Company.Affiliate;
using Core.Dtos;
using Core.Dtos.Company.Affiliate;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Company.Affiliate;
using Core.Dtos.Exceptions.Partner;
using Core.Dtos.Exceptions.Region;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Affiliate;

public sealed class AddManagerCommandHanlder
    : IRequestHandler<AddManagerCommand, Result>
{
    private readonly IAffiliateService _affiliateService;

    public AddManagerCommandHanlder(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result> Handle(
        AddManagerCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _affiliateService.AddManager(request.affiliateId, request.partnerDto);
            return Result.Success();
        }
        catch (AffiliateNotFoundException)
        {
            return Result.Failure(ErrorMessage.Create(
                "AffiliateId",
                $"Affiliate not found"
            ));
        }
        catch (PartnerNotFound)
        {
            return Result.Failure(ErrorMessage.Create(
                "PartnerEmail",
                $"Partner not found"
            ));
        }
        catch (PartnerEmailAlreadyRegistered)
        {
            return Result.Failure(ErrorMessage.Create(
                "PartnerAlreadyManager",
                $"Partner already manager"
            ));
        }
        catch (Exception ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }
    }
}
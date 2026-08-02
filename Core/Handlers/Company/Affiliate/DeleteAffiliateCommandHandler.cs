using Core.Commands.Company;
using Core.Commands.Company.Affiliate;
using Core.Dtos;
using Core.Dtos.Company;
using Core.Dtos.Company.Affiliate;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Company.Affiliate;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Affiliate;

public sealed class DeleteAffiliateCommandHandler
    : IRequestHandler<DeleteAffiliateCommand, Result<bool>>
{
    private readonly IAffiliateService _affiliateService;

    public DeleteAffiliateCommandHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result<bool>> Handle(
        DeleteAffiliateCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Result<bool>.Success(await _affiliateService.DeleteAffiliateAsync(request.affiliateId));
        }
        catch (AffiliateNotFoundException)
        {
            return Result<bool>.Failure(ErrorMessage.Create(
                "AffiliateId",
                $"Affiliate not found"
            ));
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }
    }
}
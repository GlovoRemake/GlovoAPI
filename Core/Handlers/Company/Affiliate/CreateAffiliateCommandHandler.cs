using Core.Commands.Company;
using Core.Commands.Company.Affiliate;
using Core.Dtos;
using Core.Dtos.Company;
using Core.Dtos.Company.Affiliate;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Affiliate;

public sealed class CreateAffiliateCommandHandler
    : IRequestHandler<CreateAffiliateCommand, Result<AffiliateDto>>
{
    private readonly IAffiliateService _affiliateService;

    public CreateAffiliateCommandHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result<AffiliateDto>> Handle(
        CreateAffiliateCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Result<AffiliateDto>.Success(await _affiliateService.CreateAffiliateAsync(request.companyId, request.dto));
        }
        catch (Exception ex)
        {
            return Result<AffiliateDto>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }
    }
}
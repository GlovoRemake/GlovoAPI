using Core.Dtos;
using Core.Dtos.Company;
using Core.Dtos.Company.Affiliate;
using Core.Interfaces;
using Core.Queries.Company;
using Core.Queries.Company.Affiliate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Affiliate;

public sealed class GetAllAffiliatesByRegionQueryHandler
    : IRequestHandler<GetAllAffiliatesByRegionQuery, Result<AffiliateDto[]>>
{
    private readonly IAffiliateService _affiliateService;

    public GetAllAffiliatesByRegionQueryHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result<AffiliateDto[]>> Handle(
        GetAllAffiliatesByRegionQuery request,
        CancellationToken cancellationToken)
    {


        return Result<AffiliateDto[]>.Success(await _affiliateService.GetAllAffiliatesByRegionAsync(request.companyId, request.cityId));
    }
}
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

public sealed class GetAllAffiliatesQueryHandler
    : IRequestHandler<GetAllAffiliatesQuery, Result<PagedAffiliatesDto>>
{
    private readonly IAffiliateService _affiliateService;

    public GetAllAffiliatesQueryHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result<PagedAffiliatesDto>> Handle(
        GetAllAffiliatesQuery request,
        CancellationToken cancellationToken)
    {


        return Result< PagedAffiliatesDto>.Success(await _affiliateService.GetAllAffiliatesAsync(request.companyId, request.pageNumber, request.pageSize));
    }
}
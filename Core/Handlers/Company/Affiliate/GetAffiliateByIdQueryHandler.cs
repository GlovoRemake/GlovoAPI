using Core.Dtos;
using Core.Dtos.Company.Affiliate;
using Core.Interfaces;
using Core.Queries.Company.Affiliate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Affiliate;

public sealed class GetAffiliateByIdQueryHandler
    : IRequestHandler<GetAffiliateByIdQuery, Result<AffiliateDto>>
{
    private readonly IAffiliateService _affiliateService;

    public GetAffiliateByIdQueryHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result<AffiliateDto>> Handle(
        GetAffiliateByIdQuery request,
        CancellationToken cancellationToken)
    {


        return Result<AffiliateDto>.Success(await _affiliateService.GetAffilaiteById(request.affiliateId));
    }
}